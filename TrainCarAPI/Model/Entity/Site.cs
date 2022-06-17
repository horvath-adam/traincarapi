using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TrainCarAPI.Model.Entity
{
    /// <summary>
    /// Telephely
    /// </summary>
    public class Site : AbstractEntity
    {
        /// <summary>
        /// Név/város neve
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Tulajdonos
        /// </summary>
        public Company Owner { get; set; }
        /// <summary>
        /// Postai cím
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Egyedi kódszám
        /// </summary>
        public string Code { get; set; }
    }

    public class SiteEntityTypeConfiguration : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
            builder.HasQueryFilter(e => !e.Deleted);
        }
    }
}
