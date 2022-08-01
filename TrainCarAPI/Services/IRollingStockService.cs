using TrainCarAPI.Model.Entity;

namespace TrainCarAPI.Services
{
    public interface IRollingStockService
    {
        /// <summary>
        /// Get all rolling stocks
        /// </summary>
        public IQueryable<RollingStock> GetAll();
        /// <summary>
        /// Get all rollig stocks by middleNumber
        /// </summary>
        public IQueryable<RollingStock> GetByTrackNumberMiddleNumber(string middleNumber);
        /// <summary>
        /// Get all rolling stocks by serial number
        /// </summary>
        public IQueryable<RollingStock> GetAllBySerialNumber(string serialNumber);
        /// <summary>
        /// Get all rolling stocks by site id
        /// </summary>
        public IQueryable<RollingStock> GetRollingStocksBySite(int siteId);
    }
}
