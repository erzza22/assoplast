using MVC.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MVC.Interfaces
{
    public interface ITransportatoreService
    {
        Task<IEnumerable<Transportatore>> GetTransportatore();
        Task<Transportatore> GetTransportatoreById(int id);
        Task AddTransportatore(Transportatore transportatore);
        void UpdateTransportatore(Transportatore transportatore);
        Task DeleteTransportatore(Transportatore transportatore);
        Task SaveChanges();
    }
}