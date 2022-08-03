using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
        public string Name { get; set; }
        /// <summary>
        /// Postal address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Unique code number
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Owner id
        /// </summary>
        public int OwnerId { get; set; }
        /// <summary>
        /// Owner of the site
        /// </summary>
        public Company? Owner { get; set; }
    }
}
