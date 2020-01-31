using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Entities;
using System.IO;

namespace MVC.Dtos
{
    public class UserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }

        public UserProfileUploads UserProfileUploads{ get;set;}
        public string ImagePath { get; set; }
        public byte[] Image { get; set; }
        public string Token { get; set; }
    }
}
