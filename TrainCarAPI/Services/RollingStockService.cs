using Microsoft.EntityFrameworkCore;
using TrainCarAPI.Context;
using TrainCarAPI.Model.DTO;
using TrainCarAPI.Model.Entity;
using TrainCarAPI.UnitOfWork;

namespace TrainCarAPI.Services
{
    public class RollingStockService : IRollingStockService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRollingStockUnitOfWork _rollingStockUnitOfWork;

        //private readonly TrainCarAPIDbContext _trainCarAPIDbContext;
        /*public RollingStockService(TrainCarAPIDbContext context)
        {
            _trainCarAPIDbContext = context;
        }*/

        public RollingStockService(IUnitOfWork unitOfWork, IRollingStockUnitOfWork rollingStockUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _rollingStockUnitOfWork = rollingStockUnitOfWork;
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
        /// Use UnitOfWork (related to task 8)
        /// </summary>
        /// <param name="containDeleted"></param>
        /// <returns></returns>
        private IQueryable<RollingStock> GetBasedOnContainDeleted(bool containDeleted)
        {
            return containDeleted ? _unitOfWork.GetRepository<RollingStock>().GetAll().IgnoreQueryFilters() : _unitOfWork.GetRepository<RollingStock>().GetAll();
            //return containDeleted ? _trainCarAPIDbContext.Set<RollingStock>().IgnoreQueryFilters() : _trainCarAPIDbContext.Set<RollingStock>();
        }

        /// <summary>
        /// Get aggregated rolling stocks (number of manufactured and number of deleted rolling stocks by serial number and year) (related to task 7)
        /// Use UnitOfWork (related to task 8)
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Dictionary<int, RollingStockData>> GetAggergatedRollingStocks()
        {
            Dictionary<string, Dictionary<int, RollingStockData>> aggergatedRollingStocks = new Dictionary<string, Dictionary<int, RollingStockData>>();
            _unitOfWork.GetRepository<RollingStock>().GetAll().ToList().GroupBy(rs => rs.SerialNumber).ToList().ForEach(rollingStock =>
            {
                var years = rollingStock.Select(r => r.YearOfManufacture).Distinct().ToHashSet();
                years.UnionWith(rollingStock.Where(r => r.DisposalDate != DateTime.MaxValue).Select(r => r.DisposalDate.Year).Distinct().ToHashSet());
                Dictionary<int, RollingStockData> data = new Dictionary<int, RollingStockData>();
                years.ToList().ForEach(year =>
                {
                    var manufacturedNumber = rollingStock.Where(r => r.YearOfManufacture == year).Count();
                    var deletedNumber = rollingStock.Where(r => r.DisposalDate.Year == year).Count();
                    var rollingStockData = new RollingStockData(deletedNumber, manufacturedNumber);
                    data.Add(year, rollingStockData);
                });
                aggergatedRollingStocks.Add(rollingStock.FirstOrDefault().SerialNumber, data);
            });
            return aggergatedRollingStocks;
        }

        /// <summary>
        /// Create new rolling stock (reletad to task 3)
        /// Use UnitOfWork (related to task 8)
        /// </summary>
        public async Task AddRollingStock(RollingStock rollingStock)
        {
            await _unitOfWork.GetRepository<RollingStock>().Create(rollingStock);
            await _unitOfWork.SaveChangesAsync();
            /*await _trainCarAPIDbContext.Set<RollingStock>().AddAsync(rollingStock);
            await _trainCarAPIDbContext.SaveChangesAsync();*/
        }

        /// <summary>
        /// Update rolling stock (reletad to task 3)
        /// Use UnitOfWork (related to task 8)
        /// </summary>
        public async Task UpdateRollingStock(RollingStock rollingStock)
        {
            var rollingStockToUpdate = await _unitOfWork.GetRepository<RollingStock>().GetById(rollingStock.Id);
            rollingStockToUpdate.SerialNumber = rollingStock.SerialNumber;
            rollingStockToUpdate.TrackNumber = rollingStock.TrackNumber;
            rollingStockToUpdate.YearOfManufacture = rollingStock.YearOfManufacture;
            rollingStockToUpdate.SiteId = rollingStock.SiteId;
            rollingStockToUpdate.OwnerId = rollingStock.OwnerId;
            _unitOfWork.GetRepository<RollingStock>().Update(rollingStockToUpdate);
            await _unitOfWork.SaveChangesAsync();
            /*var rollingStockToUpdate = _trainCarAPIDbContext.Set<RollingStock>().FirstOrDefault(rs => rs.Id == rollingStock.Id);
            rollingStockToUpdate.SerialNumber = rollingStock.SerialNumber;
            rollingStockToUpdate.TrackNumber = rollingStock.TrackNumber;
            rollingStockToUpdate.YearOfManufacture = rollingStock.YearOfManufacture;
            rollingStockToUpdate.SiteId = rollingStock.SiteId;
            rollingStockToUpdate.OwnerId = rollingStock.OwnerId;
            _trainCarAPIDbContext.Set<RollingStock>().Update(rollingStockToUpdate);
            await _trainCarAPIDbContext.SaveChangesAsync();*/
        }

        /// <summary>
        /// Soft delete rolling stock (reletad to task 3)
        /// Save disposal date when delete rolling stock (related to task 5)
        /// Use UnitOfWork (related to task 8)
        /// </summary>
        public async Task DeleteRollingStock(int id, DateTime? disposalDate)
        {
            var rollingStockToDelete = await _unitOfWork.GetRepository<RollingStock>().GetById(id);
            if (disposalDate != null)
            {
                rollingStockToDelete.DisposalDate = (DateTime)disposalDate;
            }
            else
            {
                rollingStockToDelete.DisposalDate = DateTime.Now;
            }
            rollingStockToDelete.Deleted = true;
            _unitOfWork.GetRepository<RollingStock>().Update(rollingStockToDelete);
            await _unitOfWork.SaveChangesAsync();

            /*var rollingStockToDelete = _trainCarAPIDbContext.Set<RollingStock>().FirstOrDefault(site => site.Id == id);
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
            await _trainCarAPIDbContext.SaveChangesAsync();*/
        }

        public IQueryable<RollingStock> GetRollingStockByYearOfManufacture(int year, bool containDeleted)
        {
            return _rollingStockUnitOfWork.GetRollingStockByYearOfManufacture(year, containDeleted);
        }
    }
}
