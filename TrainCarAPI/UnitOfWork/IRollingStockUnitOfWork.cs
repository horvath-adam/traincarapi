using TrainCarAPI.Model.Entity;

namespace TrainCarAPI.UnitOfWork
{
    public interface IRollingStockUnitOfWork
    {
        IQueryable<RollingStock> GetRollingStockByYearOfManufacture(int year, bool containDeleted);
    }
}
