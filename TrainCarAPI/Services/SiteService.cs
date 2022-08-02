using TrainCarAPI.Context;
using TrainCarAPI.Model.Entity;

namespace TrainCarAPI.Services
{
    public class SiteService: ISiteService
    {
        private readonly TrainCarAPIDbContext _trainCarAPIDbContext;
        public SiteService(TrainCarAPIDbContext context)
        {
            _trainCarAPIDbContext = context;
        }

        /// <summary>
        /// Create new site (related to task 3)
        /// </summary>
        public async Task CreateSite(Site site)
        {
            await _trainCarAPIDbContext.Set<Site>().AddAsync(site);
            await _trainCarAPIDbContext.SaveChangesAsync();
        }


        /// <summary>
        /// Update site (related to task 3)
        /// </summary>
        public async Task UpdateSite(Site site)
        {
            _trainCarAPIDbContext.Set<Site>().Update(site);
            await _trainCarAPIDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Delete site (related to task 3)
        /// </summary>
        public async Task DeleteSite(int id)
        {
            var siteToDelete = _trainCarAPIDbContext.Set<Site>().FirstOrDefault(site => site.Id == id);
            if(siteToDelete == null) return;
            siteToDelete.Deleted = true;
            _trainCarAPIDbContext.Set<Site>().Update(siteToDelete);
            await _trainCarAPIDbContext.SaveChangesAsync();
        }
    }
}
