using MVC.Entities;
using MVC.Interfaces;
using MVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Services
{
    public class TransportatoreService : ITransportatoreService
    {
        private readonly IGenericRepository<Transportatore> _genericRepository;
        public TransportatoreService(IGenericRepository<Transportatore> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task AddTransportatore(Transportatore transportatore)
        {
            try
            {
                await _genericRepository.Add(transportatore);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Transportatore>> GetTransportatore()
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

        public async Task<Transportatore> GetTransportatoreById(int id)
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

        public void UpdateTransportatore(Transportatore transportatore)
        {
            try
            {
                _genericRepository.Update(transportatore);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteTransportatore(Transportatore transportatore)
        {
            try
            {
                _genericRepository.Remove(transportatore);
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
