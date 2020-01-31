using Microsoft.AspNetCore.Http;
using MVC.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<AspNetUsers>> GetUsersAsync();
        Task<AspNetUsers> GetUserById(string id);
        Task SaveChanges();
        Task UploadImage(IFormFile image, string id);
        void SaveFile(IFormFile file, string folderName, string id);
        Task RemoveImage(string id);
        Task<Byte[]> GetImageAsByteStringByUserId(string userId);

    }
}
