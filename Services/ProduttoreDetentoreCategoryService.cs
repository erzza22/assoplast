using MVC.Entities;

using MVC.Interfaces;
using MVC.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Services
{
    public class ProduttoreDetentoreCategoryService : IProduttoreDetentoreCategoryService
    {
        private IGenericRepository<ProduttoreDetentoreCategory> _genericRepository;
        public ProduttoreDetentoreCategoryService(IGenericRepository<ProduttoreDetentoreCategory> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task AddProduttoreDetentoreCategory (ProduttoreDetentoreCategory produttoreDetentoreCategory)
        {
            try
            {
                await _genericRepository.Add(produttoreDetentoreCategory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ProduttoreDetentoreCategory>> GetProduttoreDetentoreCategories()
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

        public async Task<ProduttoreDetentoreCategory> GetProduttoreDetentoreCategoryById(int id)
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

        public void UpdateProduttoreDetentoreCategory (ProduttoreDetentoreCategory produttoreDetentoreCategory)
        {
            try
            {
                _genericRepository.Update(produttoreDetentoreCategory);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteProduttoreDetentoreCategory(ProduttoreDetentoreCategory produttoreDetentoreCategory)
        {
            try
            {
                _genericRepository.Remove(produttoreDetentoreCategory);
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

        public async Task<string> GetProduttoreDetentoreCategoryNameAsync(int id)
        {
            try
            {
                var categoryList = await GetProduttoreDetentoreCategories();
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

