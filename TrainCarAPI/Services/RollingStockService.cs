using Microsoft.EntityFrameworkCore;
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
        public IQueryable<RollingStock> GetAll(bool containDeleted)
        {
            return GetBasedOnContainDeleted(containDeleted);
        }

        /// <summary>
        /// Get a specific rolling stock by serial number
        /// </summary>
        public IQueryable<RollingStock> GetAllBySerialNumber(string serialNumber, bool containDeleted)
        {
            return GetBasedOnContainDeleted(containDeleted).ToList().Where(stock => stock.SerialNumber == serialNumber).AsQueryable();
        }
        /// <summary>
        /// Get all Rolling stock by middle number
        /// </summary>
        public IQueryable<RollingStock> GetByTrackNumberMiddleNumber(string middleNumber, bool containDeleted)
        {
            var rollingStocks = GetBasedOnContainDeleted(containDeleted);
            return rollingStocks.ToList().Where(stock => stock.getMiddleNumber() == middleNumber).AsQueryable();
        }

        /// <summary>
        /// Get a specific rolling stock by site id
        /// </summary>
        public IQueryable<RollingStock> GetRollingStocksBySite(int siteId, bool containDeleted)
        {
            return GetBasedOnContainDeleted(containDeleted).Where(stock => stock.SiteId == siteId);
        }

        /// <summary>
        /// Get rolling stocks based on containDeleted flag (related to task 4)
        /// </summary>
        /// <param name="containDeleted"></param>
        /// <returns></returns>
        private IQueryable<RollingStock> GetBasedOnContainDeleted(bool containDeleted)
        {
            return containDeleted ? _trainCarAPIDbContext.Set<RollingStock>().IgnoreQueryFilters() : _trainCarAPIDbContext.Set<RollingStock>();
        }

        /// <summary>
        /// Create new rolling stock (reletad to task 3)
        /// </summary>
        public async Task AddRollingStock(RollingStock rollingStock)
        {
            await _trainCarAPIDbContext.Set<RollingStock>().AddAsync(rollingStock);
            await _trainCarAPIDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Update rolling stock (reletad to task 3)
        /// </summary>
        public async Task UpdateRollingStock(RollingStock rollingStock)
        {
            var rollingStockToUpdate = _trainCarAPIDbContext.Set<RollingStock>().FirstOrDefault(rs => rs.Id == rollingStock.Id);
            rollingStockToUpdate.SerialNumber = rollingStock.SerialNumber;
            rollingStockToUpdate.TrackNumber = rollingStock.TrackNumber;
            rollingStockToUpdate.YearOfManufacture = rollingStock.YearOfManufacture;
            rollingStockToUpdate.SiteId = rollingStock.SiteId;
            rollingStockToUpdate.OwnerId = rollingStock.OwnerId;
            _trainCarAPIDbContext.Set<RollingStock>().Update(rollingStockToUpdate);
            await _trainCarAPIDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Soft delete rolling stock (reletad to task 3)
        /// Save disposal date when delete rolling stock (related to task 5)
        /// </summary>
        public async Task DeleteRollingStock(int id, DateTime? disposalDate)
        {
            var rollingStockToDelete = _trainCarAPIDbContext.Set<RollingStock>().FirstOrDefault(site => site.Id == id);
            if (rollingStockToDelete == null) return;
            if(disposalDate != null)
            {
                rollingStockToDelete.DisposalDate = (DateTime)disposalDate;
            } else
            {
                rollingStockToDelete.DisposalDate = DateTime.Now;
            }
            rollingStockToDelete.Deleted = true;
            _trainCarAPIDbContext.Set<RollingStock>().Update(rollingStockToDelete);
            await _trainCarAPIDbContext.SaveChangesAsync();
        }
    }
}
