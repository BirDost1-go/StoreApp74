using FCB.Models; // Adjust the namespace as necessary

namespace FCB.Repositories
{
    public interface IProductRepository:IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllProductWithOwnerAsync();
        Task<Product> GetProductWithOwnerByIdAsync(int id);
        Task<Product> GetProductByImageUrlAsync(string imageUrl);
        Task<IEnumerable<Product>> GetProductsByOwnerIdAsync(int ownerId);
        Task<bool>  ProductExistsByIdAsync(int id);
    }
}
