using MVC.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Interfaces
{
    public interface IDestinatarioCategoryService
    {
        Task AddDestinatarioCategory(DestinatarioCategory destinatarioCategory);
        Task<IEnumerable<DestinatarioCategory>> GetDestinatarioCategories();
        Task<DestinatarioCategory> GetDestinatarioCategoryById(int id);
        void UpdateDestinatarioCategory(DestinatarioCategory destinatarioCategory);
        Task DeleteDestinatarioCategory(DestinatarioCategory destinatarioCategory);
        Task SaveChanges();
        Task<string> GetReceiverCategoryNameAsync(int id);
    }
}
