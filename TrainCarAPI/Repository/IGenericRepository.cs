using Microsoft.EntityFrameworkCore;
using TrainCarAPI.Model.Entity;

namespace TrainCarAPI.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(int id);
        Task Create(TEntity entity);
        void Update(TEntity entity);
        Task Delete(int id);
        Task DeleteSoft(int id);
    }
}
