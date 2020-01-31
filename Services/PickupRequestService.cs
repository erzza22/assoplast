using MVC.Entities;
using MVC.Interfaces;
using MVC.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Services
{
    public class PickupRequestService : IPickupRequestService
    {
        private IGenericRepository<PickupRequest> _genericRepository;
        public PickupRequestService(IGenericRepository<PickupRequest> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task AddPickupRequest (PickupRequest pickupRequest)
        {
            try
            {
                await _genericRepository.Add(pickupRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<PickupRequest>> GetPickupRequests()
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

        public async Task<PickupRequest> GetPickupRequestById(int id)
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

        public void UpdatePickupRequest (PickupRequest pickupRequest)
        {
            try
            {
                _genericRepository.Update(pickupRequest);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeletePickupRequest(PickupRequest pickupRequest)
        {
            try
            {
                _genericRepository.Remove(pickupRequest);
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

