using MVC.Entities;
using MVC.Interfaces;
using MVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Services
{
    public class RequestService : IRequestService
    {
        private readonly IGenericRepository<Request> _genericRepository;
        public RequestService(IGenericRepository<Request> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task AddRequest(Request request)
        {
            try
            {
                await _genericRepository.Add(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Request>> GetRequests()
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

        public async Task<Request> GetRequestById(int id)
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

        public void UpdateRequest(Request request)
        {
            try
            {
                _genericRepository.Update(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteRequest(Request request)
        {
            try
            {
                _genericRepository.Remove(request);
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
