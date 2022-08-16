using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TrainCarAPI.Model.Entity;
using TrainCarAPI.UnitOfWork;

namespace TrainCarAPI.Services
{
    public class CacheService: ICacheService
    {
        private readonly IMemoryCache _cache; 
        private readonly IUnitOfWork _unitOfWork;

        public CacheService(IMemoryCache cache, IUnitOfWork unitOfWork)
        {
            _cache = cache;
            _unitOfWork = unitOfWork;
        }

        public void SetCache()
        {
            _unitOfWork.GetDbSet<Site>().AsNoTracking().Include(r => r.Owner).ToList().ForEach(site =>
            {
                _cache.Set(site.Code, site);
                _cache.Set(site.Id, site);
            });
        }

        public bool TryGetValue<TEntity>(object cacheKey, out TEntity value)
        {
            return _cache.TryGetValue<TEntity>(cacheKey, out value);
        }

        public void Remove(object cacheKey)
        {
            _cache.Remove(cacheKey);
        }

        public void Set(object cacheKey, object value)
        {
            _cache.Set(cacheKey, value);
        }
    }
}
