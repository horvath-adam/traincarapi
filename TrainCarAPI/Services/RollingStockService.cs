using TrainCarAPI.Context;
using TrainCarAPI.Model.Entity;

namespace TrainCarAPI.Services
{
    public class RollingStockService : IRollingStockService
    {
        private readonly TrainCarAPIDbContext _trainCarAPIDbContext;
        public RollingStockService(TrainCarAPIDbContext context)
        {
            _trainCarAPIDbContext = context;
        }

        /// <summary>
        /// Get all rolling stock using dbcontext
        /// </summary>
        public IQueryable<RollingStock> GetAll()
        {
            return _trainCarAPIDbContext.Set<RollingStock>();
        }

        /// <summary>
        /// Get a specific rolling stock by serial number
        /// </summary>
        public IQueryable<RollingStock> GetAllBySerialNumber(string serialNumber)
        {
            return _trainCarAPIDbContext.Set<RollingStock>().ToList().Where(stock => stock.SerialNumber == serialNumber).AsQueryable();
        }
        /// <summary>
        /// Get all Rolling stock by middle number
        /// </summary>
        public IQueryable<RollingStock> GetByTrackNumberMiddleNumber(string middleNumber)
        {
            var rollingStocks = _trainCarAPIDbContext.Set<RollingStock>();
            return rollingStocks.ToList().Where(stock => stock.getMiddleNumber() == middleNumber).AsQueryable();
        }

        /// <summary>
        /// Get a specific rolling stock by site id
        /// </summary>
        public IQueryable<RollingStock> GetRollingStocksBySite(int siteId)
        {
            return _trainCarAPIDbContext.Set<RollingStock>().Where(stock => stock.SiteId == siteId);
        }
    }
}
