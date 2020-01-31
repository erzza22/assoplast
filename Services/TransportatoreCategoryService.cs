using MVC.Entities;
using MVC.Interfaces;
using MVC.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Services
{
    public class TransportatoreCategoryService : ITransportatoreCategoryService
    {
        private IGenericRepository<TransportatoreCategory> _genericRepository;
        public TransportatoreCategoryService(IGenericRepository<TransportatoreCategory> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task AddTransportatoreCategory(TransportatoreCategory transportatoreCategory)
        {
            try
            {
                await _genericRepository.Add(transportatoreCategory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<TransportatoreCategory>> GetTransportatoreCategories()
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

        public async Task<TransportatoreCategory> GetTransportatoreCategoryById(int id)
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

        public void UpdateTransportatoreCategory(TransportatoreCategory transportatoreCategory)
        {
            try
            {
                _genericRepository.Update(transportatoreCategory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteTransportatoreCategory(TransportatoreCategory transportatoreCategory)
        {
            try
            {
                _genericRepository.Remove(transportatoreCategory);
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

        public async Task<string> GetTransportatoreCategoryNameAsync(int id)
        {
            try
            {
                var categoryList = await GetTransportatoreCategories();
                var CategoryName = "";
                foreach (var category in categoryList)
                {
                    if (category.Id == id)
                    {
                        CategoryName = category.Name;
                    }
                } 
                return CategoryName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

