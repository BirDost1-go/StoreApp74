using FCB.Models; // Adjust the namespace as necessary
using FCB.Repositories;
using System.Linq.Expressions;

namespace FCB.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<People>> GetAllAsync(Expression<Func<People, bool>>? filter = null);
        Task<People?> GetByIdAsync(int id);
        Task AddAsync(People entity);
        Task UpdateAsync(People entity);
        Task DeleteAsync(int id);
        Task DeleteAsync(People entity);
        Task<IEnumerable<People>> FindAsync(Expression<Func<People, bool>> predicate);
        //Task<IEnumerable<People>> GetProductWithOwnerAsync();
        //Task<Product?> GetPRoductWithOwnerByIdAsync(int id);
        Task<People?> GetByEmailAsync(string email);
        Task<People?> GetByPhoneNumberAsync(string phoneNumber);
        Task<bool> PeopleExistsByIdAsync(int id);
    }
}
