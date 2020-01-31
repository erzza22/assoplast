using MVC.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Interfaces
{
    public interface IProduttoreDetentoreService
    {
        Task AddProduttoreDetentore(ProduttoreDetentore produttoreDetentore);
        Task<IEnumerable<ProduttoreDetentore>> GetProduttoreDetentore();
        Task<ProduttoreDetentore> GetProduttoreDetentoreById(int id);
        void UpdateProduttoreDetentore(ProduttoreDetentore produttoreDetentore);
        Task DeleteProduttoreDetentore(ProduttoreDetentore produttoreDetentore);
        Task SaveChanges();
    }
}
