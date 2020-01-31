using System.Collections.Generic;
using System.Threading.Tasks;
using MVC.Entities;

namespace MVC.Interfaces
{
    public interface IRequestCategoryService
    {
        Task<IEnumerable<RequestCategory>> GetRequestCategories();
        Task<RequestCategory> GetRequestCategoryById(int id);
        Task AddRequestCategory(RequestCategory requestCategory);
        void UpdateRequestCategory(RequestCategory requestCategory);
        Task DeleteRequestCategory(RequestCategory requestCategory);
        Task<string> GetRequestCategoryNameAsync(int id);
        Task SaveChanges();
    }
}