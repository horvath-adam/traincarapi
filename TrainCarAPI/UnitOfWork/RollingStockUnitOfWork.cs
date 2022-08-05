using Microsoft.EntityFrameworkCore;
using TrainCarAPI.Context;
using TrainCarAPI.Model.Entity;

namespace TrainCarAPI.UnitOfWork
{
    public class RollingStockUnitOfWork : UnitOfWork, IRollingStockUnitOfWork 
    {
        public RollingStockUnitOfWork(TrainCarAPIDbContext trainCarAPIDbContext) : base(trainCarAPIDbContext)
        {
        }

        public IQueryable<RollingStock> GetRollingStockByYearOfManufacture(int year, bool containDeleted)
        {
            return containDeleted ?
                GetRepository<RollingStock>().GetAll().IgnoreQueryFilters().Where(rollingStock => rollingStock.YearOfManufacture == year) :
                GetRepository<RollingStock>().GetAll().Where(rollingStock => rollingStock.YearOfManufacture == year);
        }
    }
}
