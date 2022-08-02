﻿using Microsoft.AspNetCore.Mvc;
using TrainCarAPI.Model.Entity;
using TrainCarAPI.Services;

namespace TrainCarAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SiteController : Controller
    {
        private readonly ISiteService _siteService;

        public SiteController(ISiteService siteService)
        {
            _siteService = siteService;
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