using MVC.Entities;

using MVC.Interfaces;
using MVC.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Services
{
    public class ApplianceCategoryService : IApplianceCategoryService
    {
        private IGenericRepository<ApplianceCategory> _genericRepository;
        public ApplianceCategoryService(IGenericRepository<ApplianceCategory> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task AddApplianceCategory (ApplianceCategory applianceCategory)
        {
            try
            {
                await _genericRepository.Add(applianceCategory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ApplianceCategory>> GetApplianceCategories()
        {
            try
            {
                return await _genericRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApplianceCategory> GetApplianceCategoryById(int id)
        {
            try
            {
               return await _genericRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateApplianceCategory (ApplianceCategory applianceCategory)
        {
            try
            {
                _genericRepository.Update(applianceCategory);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteApplianceCategory(ApplianceCategory applianceCategory)
        {
            try
            {
                _genericRepository.Remove(applianceCategory);
                await _genericRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SaveChanges()
        {
            try
            {
                await _genericRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

