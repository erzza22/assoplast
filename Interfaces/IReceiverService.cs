using System.Collections.Generic;
using System.Threading.Tasks;
using MVC.Entities;

namespace MVC.Interfaces
{
    public interface IReceiverService
    {
        Task<IEnumerable<Receiver>> GetReceivers();
        Task<Receiver> GetReceiverById(int id);
        Task AddReceiver(Receiver receiver);
        void UpdateReceiver(Receiver receiver);
        Task DeleteReceiver(Receiver receiver);
        Task SaveChanges();
    }
}