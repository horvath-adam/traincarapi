namespace TrainCarAPI.Model.DTO
{
    public class RollingStockImportDTO
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public int YearOfManufacture { get; set; }
        public string SiteName { get; set; }

        public RollingStockImportDTO()
        {

        }
    }
}
