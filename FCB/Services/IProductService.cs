using FCB.Models;
using FCB.Repositories; 
using System.Linq.Expressions;
using System.Net;

namespace FCB.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>>? filter = null);
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product entity);
        Task UpdateAsync(Product entity);
        Task DeleteAsync(int id);
        Task DeleteAsync(Product entity);
        Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate);
        Task<IEnumerable<Product>> GetProductsWithOwnerAsync();
        Task<IEnumerable<Product>> GetProductsByOwnerIdAsync(int ownerId);

        Task<People?> GetOwnerByProductIdAsync(int ownerId);
        Task<bool> ProductExistsByIdAsync(int id);
         Task<Product> GetProductWithOwnerByIdAsync(int id);

    }
}
