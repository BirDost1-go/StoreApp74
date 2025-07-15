using System.Linq.Expressions;
using FCB.Models;
using FCB.Repositories;

namespace FCB.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IPersonRepository _personRepository;
        public ProductService(IProductRepository productRepository, IPersonRepository personRepository)
        {
            _productRepository = productRepository;
            _personRepository = personRepository;
        }
        public async Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>>? filter = null)
        {
            return await _productRepository.GetAllAsync(filter);
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Product with ID {id} not found.");

        }
        public async Task AddAsync(Product entity)
        {
            await _productRepository.AddAsync(entity);
        }
        public async Task UpdateAsync(Product entity)
        {
            await _productRepository.UpdateAsync(entity);
        }
        public async Task DeleteAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }
        public async Task DeleteAsync(Product entity)
        {
            await _productRepository.DeleteAsync(entity);
        }
        public async Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate)
        {
            return await _productRepository.FindAsync(predicate);
        }
        public async Task<IEnumerable<Product>> GetProductsWithOwnerAsync()
        {
            return await _productRepository.GetAllProductWithOwnerAsync();
        }
        public async Task<People?> GetOwnerByProductIdAsync(int ownerId)
        {
            return await _personRepository.GetByIdAsync(ownerId);
        }
        public async Task<bool> ProductExistsByIdAsync(int id)
        {
            return await _productRepository.ProductExistsByIdAsync(id);
        }
        public async Task<IEnumerable<Product>> GetProductsByOwnerIdAsync(int ownerId)
        {
            return await _productRepository.GetProductsByOwnerIdAsync(ownerId);
        }
        public async Task<Product> GetProductWithOwnerByIdAsync(int id)
        {
            return await _productRepository.GetProductWithOwnerByIdAsync(id);
        }

    }
}
