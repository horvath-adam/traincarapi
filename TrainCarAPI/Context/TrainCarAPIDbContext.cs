using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace TrainCarAPI.Context
{
    public class TrainCarAPIDbContext : DbContext
    {
        public TrainCarAPIDbContext(DbContextOptions<TrainCarAPIDbContext> options) : base(options)
        {
            Database.SetCommandTimeout(60);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.RemovePluralizingTableNameConvention();
            modelBuilder.RemoveOneToManyCascadeDeleteConvention();

            base.OnModelCreating(modelBuilder);
        }
    }

    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Define the name of the table to be the name of the entity
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                if (entity.BaseType == null && !HasAttribute(entity.ClrType, typeof(TableAttribute)))
                {
                    entity.SetTableName(entity.DisplayName());
                }
            }
        }

        /// <summary>
        /// Disable cascade deletion for one or more connections
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void RemoveOneToManyCascadeDeleteConvention(this ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;
        }

        private static bool HasAttribute(Type type, Type attributeType)
        {
            return type.GetCustomAttribute(attributeType) != null;
        }
    }
}
