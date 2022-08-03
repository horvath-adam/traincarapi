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

        [HttpGet("{containDeleted}")]
        public IQueryable<RollingStock> GetAll(bool containDeleted)
        {
            return _rollingStockService.GetAll(containDeleted);
        }

        [HttpGet("{middleNumber}/{containDeleted}")]
        public IQueryable<RollingStock> GetRollingStocksByMiddleNumber(string middleNumber, bool containDeleted)
        {
            return _rollingStockService.GetByTrackNumberMiddleNumber(middleNumber, containDeleted);
        }

        [HttpGet("{serialNumber}/{containDeleted}")]
        public IQueryable<RollingStock> GetBySerialNumber(string serialNumber, bool containDeleted)
        {
            return _rollingStockService.GetAllBySerialNumber(serialNumber, containDeleted);
        }

        [HttpGet("{siteId}/{containDeleted}")]
        public IQueryable<RollingStock> GetRollingStocksBySite(int siteId, bool containDeleted)
        {
            return _rollingStockService.GetRollingStocksBySite(siteId, containDeleted);
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
        public async Task DeleteRollingStock(int id, [FromQuery] DateTime? disposalDate)
        {
            await _rollingStockService.DeleteRollingStock(id, disposalDate);
        }

    }
}
