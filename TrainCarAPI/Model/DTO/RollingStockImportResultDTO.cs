namespace TrainCarAPI.Model.DTO
{
    public class RollingStockImportResultDTO
    {
        public IList<RollingStockImportDTO> rollingStocks { get; set; }
        public int Count { get; set; }

    }
}
