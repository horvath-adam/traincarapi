using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainCarAPI.Model.DTO;
using TrainCarAPI.Model.Entity;
using TrainCarAPI.Services;

namespace TrainCarAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SiteController : Controller
    {
        private readonly ISiteService _siteService;

        public SiteController(ISiteService siteService)
        {
            _siteService = siteService;
        }

        [HttpGet("{code}")]
        public ExtendedSiteDTO GetSiteByCode(string code)
        {
            return _siteService.GetSiteByCode(code);
        }

        [HttpPost]
        public async Task CreateSite(Site site)
        {
            await _siteService.CreateSite(site);
        }

        [HttpPut]
        public async Task UpdateSite(Site site)
        {
            await _siteService.UpdateSite(site);
        }

        [HttpDelete("{id}")]
        public async Task DeleteSite(int id)
        {
            await _siteService.DeleteSite(id);
        }
    }
}
