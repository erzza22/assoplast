using Microsoft.AspNetCore.Identity;
using MVC.Entities;
using MVC.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace MVC.Services
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<AspNetUsers> _genericRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthService(IGenericRepository<AspNetUsers> genericRepository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _genericRepository = genericRepository;
            _userManager = userManager;
            _signInManager = signInManager; 
        }

        public async Task<AspNetUsers> Login(string username, string password)
        {
            try
            {
                var user = await _genericRepository.FindOne(x => x.UserName.ToLower() == username.ToLower());

                if (user == null)
                    return null;

                var result = await _signInManager.PasswordSignInAsync(user.Email, password, false, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    return user;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<AspNetUsers> Register(AspNetUsers user, string password)
        {

            try
            {
                var userIdentity = new IdentityUser { UserName = user.UserName, Email = user.UserName };
                var result = await _userManager.CreateAsync(userIdentity, password);

                if (result.Succeeded)
                {
                    var userToReturn = await _genericRepository.FindOne(x => x.UserName.ToLower() == userIdentity.UserName);
                    return userToReturn;
                }
                else
                {
                    return null;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> Logoff()
        {
           await _signInManager.SignOutAsync();
            return true;
        }

        public async Task<bool> UserExists(string username)
        {
            try
            {
                var users = await _genericRepository.GetAll();

                if (users.Any(x => x.UserName == username))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
               throw ex;
            }

        }
    }
}
