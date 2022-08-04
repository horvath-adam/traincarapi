namespace TrainCarAPI.Model.DTO
{
    public class RollingStockData
    {
        public int Deleted { get; set; }
        public int Manufactured { get; set; }

        public RollingStockData(int deleted, int manufactured)
        {
            Deleted = deleted;
            Manufactured = manufactured;
        }
    }
}
