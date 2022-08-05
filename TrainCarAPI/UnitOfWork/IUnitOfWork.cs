using Microsoft.EntityFrameworkCore;
using TrainCarAPI.Model.Entity;
using TrainCarAPI.Repository;

namespace TrainCarAPI.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : AbstractEntity;
        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : AbstractEntity;
        int SaveChanges();
        Task SaveChangesAsync();
        DbContext Context();
    }
}
