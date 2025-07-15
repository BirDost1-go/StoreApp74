using FCB.Models;
using FCB.Repositories;
using Microsoft.EntityFrameworkCore; // Adjust the namespace as necessary

namespace FCB.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllProductWithOwnerAsync()
        {
            return await _context.Products.Include(p => p.Owner).ToListAsync();
        }
        public async Task<Product> GetProductWithOwnerByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new KeyNotFoundException($"Product with ID {id} not found.");
        }
        public async Task<IEnumerable<Product>> GetProductsByOwnerIdAsync(int ownerId)
        {
            return await _context.Products
                .Where(p => p.OwnerId == ownerId)
                .ToListAsync();
        }
        public async Task<Product> GetProductByImageUrlAsync(string imageUrl)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.ImageUrl == imageUrl)
                 ?? throw new KeyNotFoundException($"Product with ImageUrl {imageUrl} not found.");   
        }
        public async Task<bool> ProductExistsByIdAsync(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }
    }
}
