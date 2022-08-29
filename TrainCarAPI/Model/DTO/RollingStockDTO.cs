using TrainCarAPI.Model.Entity;

namespace TrainCarAPI.Model.DTO
{
    public class RollingStockDTO
    {
        public string SerialNumber { get; set; }
        public int YearOfManufacture { get; set; }
        public string TrackNumber { get; set; }
        public int OwnerId { get; set; }
        public int SiteId { get; set; }

        public RollingStockDTO()
        {

        }
    }
}
