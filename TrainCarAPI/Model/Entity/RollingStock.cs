using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainCarAPI.Attributes;

namespace TrainCarAPI.Model.Entity
{
    /// <summary>
    /// The RollingStock class extends the AbstractEntity.
    /// </summary>
    public class RollingStock : AbstractEntity
    {
        [Required]
        public string SerialNumber { get; set; }

        [Required]
        public int YearOfManufacture { get; set; }

        [Required]
        [TrackNumberValidation]
        public string TrackNumber { get; set; }
        /// <summary>
        /// Disposal date related to task 5
        /// </summary>
        public DateTime DisposalDate { get; set; }

        public int OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public Company? Owner { get; set; }

        public int SiteId { get; set; }

        [ForeignKey("SiteId")]
        public Site? Site { get; set; }
        public string getMiddleNumber()
        {
            return this.TrackNumber.Split(" ")[2];
        }
    }

    public class RollingStockEntityTypeConfiguration : IEntityTypeConfiguration<RollingStock>
    {
        public void Configure(EntityTypeBuilder<RollingStock> builder)
        {
            /// <summary>
            /// Global query filter for rolling stocks (related to task 4)
            /// </summary>
            builder.HasQueryFilter(e => !e.Deleted);
            /// <summary>
            /// Add default value (DateTime.Max) to RollingStock table DisposalDate field (related to task 5)
            /// </summary>
            builder.Property(p => p.DisposalDate).HasDefaultValue(DateTime.MaxValue);
        }
    }
}
