using MVC.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Interfaces
{
    public interface IProduttoreDetentoreCategoryService
    {
        Task AddProduttoreDetentoreCategory(ProduttoreDetentoreCategory produttoreDentoreCategory);
        Task<IEnumerable<ProduttoreDetentoreCategory>> GetProduttoreDetentoreCategories();
        Task<ProduttoreDetentoreCategory> GetProduttoreDetentoreCategoryById(int id);
        void UpdateProduttoreDetentoreCategory(ProduttoreDetentoreCategory produttoreDetentoreCategory);
        Task DeleteProduttoreDetentoreCategory(ProduttoreDetentoreCategory produttoreDetentoreCategory);
        Task SaveChanges();
        Task<string> GetProduttoreDetentoreCategoryNameAsync(int id);
    }
}
