using Microsoft.EntityFrameworkCore;
using TrainCarAPI.Context;
using TrainCarAPI.Model.Entity;

namespace TrainCarAPI.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : AbstractEntity
    {

        private readonly TrainCarAPIDbContext _trainCarAPIDbContext;
        protected readonly DbSet<TEntity> DbSet;
        public GenericRepository(TrainCarAPIDbContext trainCarAPIDbContext)
        {
            _trainCarAPIDbContext = trainCarAPIDbContext;
            DbSet = _trainCarAPIDbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public async Task<TEntity> GetById(int id)
        {
            return await DbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            var createdEntity = await DbSet.AddAsync(entity);
            return createdEntity.Entity;
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            DbSet.Remove(entity);
        }
         
        public async Task<TEntity> DeleteSoft(int id)
        {
            var entity = await GetById(id);
            entity.Deleted = true;
            Update(entity);
            return entity;
        }
    }
}
