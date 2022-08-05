using TrainCarAPI.Model.DTO;
using TrainCarAPI.Model.Entity;

namespace TrainCarAPI.Services
{
    public interface IRollingStockService
    {
        /// <summary>
        /// Get all rolling stocks
        /// </summary>
        public IQueryable<RollingStock> GetAll(bool containDeleted);
        /// <summary>
        /// Get all rollig stocks by middleNumber
        /// </summary>
        public IQueryable<RollingStock> GetByTrackNumberMiddleNumber(string middleNumber, bool containDeleted);
        /// <summary>
        /// Get all rolling stocks by serial number
        /// </summary>
        public IQueryable<RollingStock> GetAllBySerialNumber(string serialNumber, bool containDeleted);

        /// <summary>
        /// Get all rolling stocks by site id
        /// </summary>
        public IQueryable<RollingStock> GetRollingStocksBySite(int siteId, bool containDeleted);

        /// <summary>
        /// Get aggregated rolling stocks (number of manufactured and number of deleted rolling stocks by serial number and year) (related to task 7)
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Dictionary<int, RollingStockData>> GetAggergatedRollingStocks();

        /// <summary>
        /// Get rolling stock by year of manufacture (related to task 9)
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public IQueryable<RollingStock> GetRollingStockByYearOfManufacture(int year, bool containDeleted);

        /// <summary>
        /// Create new rolling stock (reletad to task 3)
        /// </summary>
        public Task AddRollingStock(RollingStock rollingStock);
        /// <summary>
        /// Update rolling stock (reletad to task 3)
        /// </summary>
        public Task UpdateRollingStock(RollingStock rollingStock);
        /// <summary>
        /// Soft delete rolling stock (reletad to task 3)
        /// </summary>
        public Task DeleteRollingStock(int id, DateTime? disposalDate);
    }
}
