using MVC.Entities;
using MVC.Interfaces;
using MVC.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Services
{
    public class ProduttoreDetentoreService : IProduttoreDetentoreService
    {
        private IGenericRepository<ProduttoreDetentore> _genericRepository;
        public ProduttoreDetentoreService(IGenericRepository<ProduttoreDetentore> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task AddProduttoreDetentore(ProduttoreDetentore produttoreDetentore)
        {
            try
            {
                await _genericRepository.Add(produttoreDetentore);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ProduttoreDetentore>> GetProduttoreDetentore()
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

        public async Task<ProduttoreDetentore> GetProduttoreDetentoreById(int id)
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

        public void UpdateProduttoreDetentore (ProduttoreDetentore produttoreDetentore)
        {
            try
            {
                _genericRepository.Update(produttoreDetentore);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteProduttoreDetentore(ProduttoreDetentore produttoreDetentore)
        {
            try
            {
                _genericRepository.Remove(produttoreDetentore);
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

