using MVC.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Interfaces
{
    public interface IApplianceCategoryService
    {
        Task AddApplianceCategory(ApplianceCategory applianceCategory);
        Task<IEnumerable<ApplianceCategory>> GetApplianceCategories();
        Task<ApplianceCategory> GetApplianceCategoryById(int id);
        void UpdateApplianceCategory(ApplianceCategory applianceCategory);
        Task DeleteApplianceCategory(ApplianceCategory applianceCategory);
        Task SaveChanges();
    }
}
