using TrainCarAPI.Model.DTO;
using TrainCarAPI.Model.Entity;

namespace TrainCarAPI.Services
{
    public interface ISiteService
    {
        /// <summary>
        /// Get aggregated site data by site code (related to task 6)
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ExtendedSiteDTO GetSiteByCode(string code);
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
