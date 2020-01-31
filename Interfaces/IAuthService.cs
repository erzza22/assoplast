using MVC.Entities;
using System.Threading.Tasks;


namespace MVC.Services
{
    public interface IAuthService
    {
        Task<AspNetUsers> Login(string username, string password);
        Task<AspNetUsers> Register(AspNetUsers user, string password);
        Task<bool> UserExists(string username);
        Task<bool> Logoff();
    }
}