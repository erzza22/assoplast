using MVC.Entities;
using MVC.Interfaces;
using MVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Services
{
    public class UserUploadService: IUserUploadService
    {
        private readonly IGenericRepository<UserProfileUploads> _genericRepository;
        public UserUploadService(IGenericRepository<UserProfileUploads> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task AddProfileUploadAsync(UserProfileUploads upload)
        {

            try
            {
                await _genericRepository.Add(upload);

                await _genericRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateUserUploadAsync(UserProfileUploads upload)
        {
            try
            {
                _genericRepository.Update(upload);
                await _genericRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteUserUploadAsync(UserProfileUploads upload)
        {
            try
            {
                _genericRepository.Remove(upload);
                await _genericRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<UserProfileUploads> GetUserUploadByUserIdAsync(string userId)
        {
            try
            {
                var upload = await _genericRepository.FindOne(x => x.UserId == userId);
                return upload;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetPathOfUserUploadAsync(string userId)
        {
            try
            {
                var upload = await _genericRepository.FindOne(x => x.UserId == userId);
                if (upload != null)
                    return upload.Path;
                else return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
