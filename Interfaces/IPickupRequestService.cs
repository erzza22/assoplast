using MVC.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Interfaces
{
    public interface IPickupRequestService
    {
        Task AddPickupRequest(PickupRequest pickupRequest);
        Task<IEnumerable<PickupRequest>> GetPickupRequests();
        Task<PickupRequest> GetPickupRequestById(int id);
        void UpdatePickupRequest(PickupRequest pickupRequest);
        Task DeletePickupRequest(PickupRequest pickupRequest);
        Task SaveChanges();
    }
}