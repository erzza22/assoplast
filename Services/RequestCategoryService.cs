using MVC.Entities;
using MVC.Interfaces;
using MVC.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Services
{
    public class RequestCategoryService : IRequestCategoryService
    {
        private IGenericRepository<RequestCategory> _genericRepository;
        public RequestCategoryService(IGenericRepository<RequestCategory> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task AddRequestCategory(RequestCategory requestCategory)
        {
            try
            {
                await _genericRepository.Add(requestCategory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<RequestCategory>> GetRequestCategories()
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

        public async Task<RequestCategory> GetRequestCategoryById(int id)
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

        public void UpdateRequestCategory(RequestCategory requestCategory)
        {
            try
            {
                _genericRepository.Update(requestCategory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteRequestCategory(RequestCategory requestCategory)
        {
            try
            {
                _genericRepository.Remove(requestCategory);
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

        public async Task<string> GetRequestCategoryNameAsync(int id)
        {
            try
            {
                var categoryList = await GetRequestCategories();
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
