using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MVC.Entities;
using MVC.Interfaces;
using MVC.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MVC.Services
{
    public class UsersService : IUsersService
    {
        private IGenericRepository<AspNetUsers> _genericRepository;
       
        private readonly AssoplastPlannerContext _context;
        private readonly IUserUploadService _userUploadService;
        private readonly IHostingEnvironment _env;

        public UsersService(IGenericRepository<AspNetUsers> genericRepository, AssoplastPlannerContext context, IUserUploadService userUploadService, IHostingEnvironment env)
        {
            _genericRepository = genericRepository;
            _context = context;
            _userUploadService = userUploadService;
            _env = env;
        }

        public async Task<IEnumerable<AspNetUsers>> GetUsersAsync()
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

        public async Task<AspNetUsers> GetUserById(string id)
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

        public async Task UploadImage(IFormFile image, string id)
        {
            try
            {
           
                var uploadPath = $"uploads\\uploads\\{id}\\Image\\{image.FileName}";
                try
                {
                    SaveFile(image, "uploads", id.ToString());
                }
                catch (Exception ex)
                {
                    throw ex;

                }

                var userUpload = await  _userUploadService.GetUserUploadByUserIdAsync(id);



                if (userUpload != null)
                {
                    userUpload.Path = uploadPath;
                    await _userUploadService.UpdateUserUploadAsync(userUpload);
                    await _genericRepository.SaveChangesAsync();

                }

                else
                {
                    var userPhoto = new UserProfileUploads
                    {
                        UserId = id,
                        Name = Path.GetFileName(image.FileName),
                        Path = uploadPath
                    };
                    await _userUploadService.AddProfileUploadAsync(userPhoto);
                    await _genericRepository.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RemoveImage(string id)
        {
            try
            {
                var userUpload = await _userUploadService.GetUserUploadByUserIdAsync(id);
                if (userUpload != null)
                {
                    await _userUploadService.DeleteUserUploadAsync(userUpload);
                    await _genericRepository.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task <Byte[]> GetImageAsByteStringByUserId (string userId)
        {
            var userUploads = await _userUploadService.GetPathOfUserUploadAsync(userId);
            byte[] array = new byte[1024];
            if (!String.IsNullOrEmpty(userUploads))
            {
                var path = Path.Combine(_env.WebRootPath, userUploads);

                var sourceStream = System.IO.File.OpenRead(path);

                using (var memoryStream = new MemoryStream())
                {
                    sourceStream.CopyTo(memoryStream);
                    array = memoryStream.ToArray();
                }

                return array;
            }
            else return null;
        }

        public void SaveFile(IFormFile file, string folderName, string id)
        {
            try
            {
                var filePath = Path.Combine(_env.WebRootPath, "uploads", folderName, id, "Image");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
           (new FileInfo(filePath)).Directory.Create();

                using (var fileStream = new FileStream(Path.Combine(filePath, file.FileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);

                    fileStream.Close();
                }
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
