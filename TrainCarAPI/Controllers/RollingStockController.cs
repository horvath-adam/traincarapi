using Microsoft.AspNetCore.Mvc;
using TrainCarAPI.Model.Entity;
using TrainCarAPI.Services;

namespace TrainCarAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RollingStockController : Controller
    {
        private readonly IRollingStockService _rollingStockService;

        public RollingStockController(IRollingStockService rollingStockService)
        {
            _rollingStockService = rollingStockService;
        }

        [HttpGet]
        public IQueryable<RollingStock> GetAll()
        {
            return _rollingStockService.GetAll();
        }

        [HttpGet("{middleNumber}")]
        public IQueryable<RollingStock> GetRollingStocksByMiddleNumber(string middleNumber)
        {
            return _rollingStockService.GetByTrackNumberMiddleNumber(middleNumber);
        }

        [HttpGet("{serialNumber}")]
        public IQueryable<RollingStock> GetBySerialNumber(string serialNumber)
        {
            return _rollingStockService.GetAllBySerialNumber(serialNumber);
        }

        [HttpGet("{siteId}")]
        public IQueryable<RollingStock> GetRollingStocksBySite(int siteId)
        {
            return _rollingStockService.GetRollingStocksBySite(siteId);
        }

        [HttpPost]
        public async Task AddRollingStock(RollingStock rollingStock)
        {
            await _rollingStockService.AddRollingStock(rollingStock);
        }

        [HttpPut]
        public async Task UpdateRollingStock(RollingStock rollingStock)
        {
            await _rollingStockService.UpdateRollingStock(rollingStock);
        }

        [HttpDelete("{id}")]
        public async Task DeleteRollingStock(int id)
        {
            await _rollingStockService.DeleteRollingStock(id);
        }

    }
}
