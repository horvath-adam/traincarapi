﻿using Microsoft.EntityFrameworkCore;
using TrainCarAPI.Context;
using TrainCarAPI.Model.DTO;
using TrainCarAPI.Model.Entity;
using TrainCarAPI.UnitOfWork;

namespace TrainCarAPI.Services
{
    public class SiteService: ISiteService
    {
        private readonly TrainCarAPIDbContext _trainCarAPIDbContext;
        private readonly IRollingStockService _rollingStockService;
        private readonly IUnitOfWork _unitOfWork;
        public SiteService(TrainCarAPIDbContext context, IRollingStockService rollingStockService, IUnitOfWork unitOfWork)
        {
            _trainCarAPIDbContext = context;
            _rollingStockService = rollingStockService;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get aggregated site data by site code (related to task 6)
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ExtendedSiteDTO GetSiteByCode(string code)
        {
            var site = _unitOfWork.GetDbSet<Site>().Include(s => s.Owner).FirstOrDefault(s => s.Code == code);
            if (site == null)
                throw new Exception("There is no site with this code " + code + "!");
            var rollingStocks = _rollingStockService.GetRollingStocksBySite(site.Id, true).ToList();
            var extendedSiteDTO = new ExtendedSiteDTO(site.Name, site.Owner.Name);
            rollingStocks.GroupBy(rs => rs.SerialNumber).ToList().ForEach(rollingStockBySerialNumber =>
            {
                double averageYear = rollingStockBySerialNumber.Average(rollingStock => rollingStock.YearOfManufacture);
                int count = rollingStockBySerialNumber.Count();
                int numberOfDeleted = rollingStockBySerialNumber.Where(rollingStock => rollingStock.Deleted).Count();
                extendedSiteDTO.SiteDatas.Add(rollingStockBySerialNumber.FirstOrDefault().SerialNumber, new SiteData(count, averageYear, numberOfDeleted));
            });
            return extendedSiteDTO;
        }

        /// <summary>
        /// Create new site (related to task 3)
        /// Use UnitOfWork (related to task 8)
        /// </summary>
        public async Task CreateSite(Site site)
        {
            await _unitOfWork.GetRepository<Site>().Create(site);
            await _unitOfWork.SaveChangesAsync();
            /*await _trainCarAPIDbContext.Set<Site>().AddAsync(site);
            await _trainCarAPIDbContext.SaveChangesAsync();*/
        }


        /// <summary>
        /// Update site (related to task 3)
        /// Use UnitOfWork (related to task 8)
        /// </summary>
        public async Task UpdateSite(Site site)
        {
            _unitOfWork.GetRepository<Site>().Update(site);
            await _unitOfWork.SaveChangesAsync();
            /* _trainCarAPIDbContext.Set<Site>().Update(site);
             await _trainCarAPIDbContext.SaveChangesAsync();*/
        }

        /// <summary>
        /// Delete site (related to task 3)
        /// Use UnitOfWork (related to task 8)
        /// </summary>
        public async Task DeleteSite(int id)
        { 
            await _unitOfWork.GetRepository<Site>().DeleteSoft(id);
            await _unitOfWork.SaveChangesAsync();
            /*var siteToDelete = _trainCarAPIDbContext.Set<Site>().FirstOrDefault(site => site.Id == id);
            if(siteToDelete == null) return;
            siteToDelete.Deleted = true;
            _trainCarAPIDbContext.Set<Site>().Update(siteToDelete);
            await _trainCarAPIDbContext.SaveChangesAsync();*/
        }
    }
}
