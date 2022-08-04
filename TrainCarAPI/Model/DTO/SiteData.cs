namespace TrainCarAPI.Model.DTO
{
    public class SiteData
    {
        /// <summary>
        /// Number of the rolling stock by serial number
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Avarage YearOfManufacturing by rolling stock serial number
        /// </summary>
        public double AvgYear { get; set; }

        /// <summary>
        /// Number of the deleted rolling stocks by serial number
        /// </summary>
        public int NumberOfDeleted { get; set; }

        public SiteData(int count, double avgYear, int numberOfDeleted)
        {
            Count = count;
            AvgYear = avgYear;
            NumberOfDeleted = numberOfDeleted;
        }
    }
}
