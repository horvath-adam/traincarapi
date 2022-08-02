using TrainCarAPI.Model.Entity;

namespace TrainCarAPI.Services
{
    public interface ISiteService
    {
        /// <summary>
        /// Create new site (related to task 3)
        /// </summary>
        public Task CreateSite(Site site);
        /// <summary>
        /// Update site (related to task 3)
        /// </summary>
        public Task UpdateSite(Site site);
        /// <summary>
        /// Delete site (related to task 3)
        /// </summary>
        public Task DeleteSite(int id);
    }
}
