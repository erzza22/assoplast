using System.Collections.Generic;
using System.Threading.Tasks;
using MVC.Entities;

namespace MVC.Interfaces
{
    public interface IRequestService
    {
        Task AddRequest(Request request);
        Task DeleteRequest(Request request);
        Task<Request> GetRequestById(int id);
        Task<IEnumerable<Request>> GetRequests();
        Task SaveChanges();
        void UpdateRequest(Request request);
    }
}