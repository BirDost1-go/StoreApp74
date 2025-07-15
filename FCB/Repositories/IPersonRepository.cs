using FCB.Models; // Adjust the namespace as necessary

namespace FCB.Repositories
{
    public interface IPersonRepository:IRepository<People>
    {
        Task<IEnumerable<People>> GetProductWithOwnerAsync();    
        //Task<Product?> GetPRoductWithOwnerByIdAsync(int id);
        Task<People?> GetByEmailAsync(string email);
        Task<People?> GetByPhoneNumberAsync(string phoneNumber);
        Task<bool> PeopleExistsByIdAsync(int id);
    }
}
