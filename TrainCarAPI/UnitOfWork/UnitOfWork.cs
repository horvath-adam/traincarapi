using Microsoft.EntityFrameworkCore;
using TrainCarAPI.Context;
using TrainCarAPI.Model.Entity;
using TrainCarAPI.Repository;

namespace TrainCarAPI.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TrainCarAPIDbContext _trainCarAPIDbContext;
        private Dictionary<Type, object> _repositories;


        public UnitOfWork(TrainCarAPIDbContext trainCarAPIDbContext)
        {
            _trainCarAPIDbContext = trainCarAPIDbContext;
        }

        public DbContext Context()
        {
            return _trainCarAPIDbContext;
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : AbstractEntity
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new GenericRepository<TEntity>(_trainCarAPIDbContext);
            }

            return (IGenericRepository<TEntity>)_repositories[type];

        }

        public int SaveChanges()
        {
            return _trainCarAPIDbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _trainCarAPIDbContext.Dispose();
            }
        }

        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : AbstractEntity
        {
            return _trainCarAPIDbContext.Set<TEntity>();
        }

        public Task SaveChangesAsync()
        {
            return _trainCarAPIDbContext.SaveChangesAsync();
        }
    }
}
