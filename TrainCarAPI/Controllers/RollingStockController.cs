﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainCarAPI.Model.DTO;
using TrainCarAPI.Model.Entity;
using TrainCarAPI.Services;

namespace TrainCarAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles="Admin,User")]
    public class RollingStockController : Controller
    {
        private readonly IRollingStockService _rollingStockService;

        public RollingStockController(IRollingStockService rollingStockService)
        {
            _rollingStockService = rollingStockService;
        }

        [AllowAnonymous]
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

        [HttpGet("{year}/{containDeleted}")]
        public IQueryable<RollingStock> GetRollingStockByYearOfManufacture(int year, bool containDeleted)
        {
            return _rollingStockService.GetRollingStockByYearOfManufacture(year, containDeleted);
        }

        [HttpGet]
        public Dictionary<string, Dictionary<int, RollingStockData>> GetAggergatedRollingStocks()
        {
            return _rollingStockService.GetAggergatedRollingStocks();
        }

        [Authorize(Roles = "User", Policy= "RailwayWorkerUser")]
        [HttpGet]
        public IEnumerable<RollingStock> GetSecondClassRollingStocks()
        {
            return _rollingStockService.GetSecondClassRollingStocks();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task AddRollingStock(RollingStock rollingStock)
        {
            await _rollingStockService.AddRollingStock(rollingStock);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task UpdateRollingStock(RollingStock rollingStock)
        {
            await _rollingStockService.UpdateRollingStock(rollingStock);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task DeleteRollingStock(int id, [FromQuery] DateTime? disposalDate)
        {
            await _rollingStockService.DeleteRollingStock(id, disposalDate);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ImportRollingStocks([FromForm(Name = "file")] IFormFile file)
        {
            _rollingStockService.Import(file);
            return Accepted("Accepted", new { });
        }

    }
}
