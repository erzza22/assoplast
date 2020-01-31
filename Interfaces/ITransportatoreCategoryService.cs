using MVC.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Interfaces
{
    public interface ITransportatoreCategoryService
    {
        Task AddTransportatoreCategory(TransportatoreCategory transportatoreCategory);
        Task<IEnumerable<TransportatoreCategory>> GetTransportatoreCategories();
        Task<TransportatoreCategory> GetTransportatoreCategoryById(int id);
        void UpdateTransportatoreCategory(TransportatoreCategory transportatoreCategory);
        Task DeleteTransportatoreCategory(TransportatoreCategory transportatoreCategory);
        Task SaveChanges();
        Task<string> GetTransportatoreCategoryNameAsync(int id);
    }
}
