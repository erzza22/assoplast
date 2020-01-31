using MVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Interfaces
{
    public interface IUserUploadService
    {
        Task AddProfileUploadAsync(UserProfileUploads upload);
        Task UpdateUserUploadAsync(UserProfileUploads upload);
        Task DeleteUserUploadAsync(UserProfileUploads upload);
        Task<UserProfileUploads> GetUserUploadByUserIdAsync(string userId);
        Task<string> GetPathOfUserUploadAsync(string userId);
    }
}
