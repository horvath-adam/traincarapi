namespace TrainCarAPI.Services
{
    public interface ICacheService
    {
        void SetCache();
        bool TryGetValue<TEntity>(object cacheKey, out TEntity value);
        void Set(object cacheKey, object value);
        void Remove(object cacheKey);
    }
}
