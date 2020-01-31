using MVC.Entities;
using MVC.Interfaces;
using MVC.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Services
{
    public class DestinatarioCategoryService : IDestinatarioCategoryService
    {
        private IGenericRepository<DestinatarioCategory> _genericRepository;
        public DestinatarioCategoryService(IGenericRepository<DestinatarioCategory> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task AddDestinatarioCategory (DestinatarioCategory destinatarioCategory)
        {
            try
            {
                await _genericRepository.Add(destinatarioCategory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DestinatarioCategory>> GetDestinatarioCategories()
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

        public async Task<DestinatarioCategory> GetDestinatarioCategoryById(int id)
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

        public void UpdateDestinatarioCategory (DestinatarioCategory destinatarioCategory)
        {
            try
            {
                _genericRepository.Update(destinatarioCategory);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteDestinatarioCategory(DestinatarioCategory destinatarioCategory)
        {
            try
            {
                _genericRepository.Remove(destinatarioCategory);
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

        public async Task<string> GetReceiverCategoryNameAsync(int id)
        {
            try
            {
                var categoryList = await GetDestinatarioCategories();
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

