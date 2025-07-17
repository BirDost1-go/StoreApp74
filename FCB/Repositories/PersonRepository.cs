using FCB.Models; // Adjust the namespace as necessary
using Microsoft.EntityFrameworkCore; // Adjust the namespace as necessary
using System.Linq.Expressions;

namespace FCB.Repositories
{
    public class PersonRepository :  IPersonRepository
    {
        private readonly ApplicationDbContext _context;
        public PersonRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<People>> GetProductWithOwnerAsync()
        {
            return await _context.People.Include(p => p.Products).ToListAsync();
        }
        public async Task<People?> GetByEmailAsync(string email)
        {
            return await _context.People.FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<People?> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.People.FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber);
        }
        public async Task<IEnumerable<People>> GetAllAsync(Expression<Func<People, bool>>? filter)
        {
            if (filter != null)
            {
                return await _context.People.Where(filter).ToListAsync();
            }
            else
            {
                return await _context.People.ToListAsync();
            }
        }
        public async Task<People?> GetByIdAsync(int id)
        {
            return await _context.People.FindAsync(id);
        }
        public async Task AddAsync(People entity)
        {
            await _context.People.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(People entity)
        {
            _context.People.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Entity with ID {id} not found.");
            _context.People.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(People entity)
        {
            _context.People.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<People>> FindAsync(Expression<Func<People, bool>> predicate)
        {
            return await _context.People.Where(predicate).ToListAsync();
        }
        public async Task<bool> PeopleExistsByIdAsync(int id)
        {
            return await _context.People.AnyAsync(p => p.Id == id);
        }
    }
}