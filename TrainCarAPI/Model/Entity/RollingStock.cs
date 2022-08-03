using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TrainCarAPI.Model.Entity
{
    /// <summary>
    /// The RollingStock class extends the AbstractEntity.
    /// </summary>
    public class RollingStock : AbstractEntity
    {
        public string SerialNumber { get; set; }
        public int YearOfManufacture { get; set; }
        public string TrackNumber { get; set; }
        /// <summary>
        /// Disposal date related to task 5
        /// </summary>
        public DateTime DisposalDate { get; set; }
        public int OwnerId { get; set; }
        public Company? Owner { get; set; }
        public int SiteId { get; set; }
        public Site? Site { get; set; }
        public string getMiddleNumber()
        {
            return this.TrackNumber.Split(" ")[2];
        }
    }
}
