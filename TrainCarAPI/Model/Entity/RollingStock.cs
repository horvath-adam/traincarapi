using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TrainCarAPI.Model.Entity
{
    /// <summary>
    /// Vasúti kocsi
    /// </summary>
    public class RollingStock : AbstractEntity
    {
        /// <summary>
        /// Sorozatjel
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// Gyártási év
        /// </summary>
        public int YearOfManufacture { get; set; }
        /// <summary>
        /// Pályaszám
        /// </summary>
        public string TrackNumber { get; set; }
        /// <summary>
        /// Tulajdonos társaság
        /// </summary>
        public Company Owner { get; set; }
        /// <summary>
        /// Telephely
        /// </summary>
        public Site Site { get; set; }
    }

    public class RollingStockEntityTypeConfiguration : IEntityTypeConfiguration<RollingStock>
    {
        public void Configure(EntityTypeBuilder<RollingStock> builder)
        {
            builder.HasQueryFilter(e => !e.Deleted);
        }
    }
}
