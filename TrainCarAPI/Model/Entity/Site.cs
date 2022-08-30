using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainCarAPI.Model.Entity
{
    /// <summary>
    /// Site of the Company and/or the RollingStock
    /// The Site class extends the AbstractEntity.
    /// </summary>
    public class Site : AbstractEntity
    {
        /// <summary>
        /// Site name
        /// </summary>
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        /// <summary>
        /// Postal address
        /// </summary>
        [Required]
        [MaxLength(250)]
        public string Address { get; set; }
        /// <summary>
        /// Unique code number
        /// </summary>
        [Required]
        public string Code { get; set; }
        /// <summary>
        /// Owner id
        /// </summary>
        public int? OwnerId { get; set; }
        /// <summary>
        /// Owner of the site
        /// </summary>
        [ForeignKey("OwnerId")]
        public Company? Owner { get; set; }
    }

    public class SiteEntityTypeConfiguration : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
        }
    }
}
