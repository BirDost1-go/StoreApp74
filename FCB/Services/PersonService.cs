using FCB.Models;
using FCB.Repositories;
using System.Linq.Expressions;  

namespace FCB.Services
{
    public class PersonService:IPersonService
    {
        private readonly IPersonRepository _personRepository;
        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public async Task<IEnumerable<People>> GetAllAsync(Expression<Func<People, bool>>? filter = null)
        {
            return await _personRepository.GetAllAsync(filter);
        }
        public async Task<People?> GetByIdAsync(int id)
        {
            return await _personRepository.GetByIdAsync(id);
        }
        public async Task AddAsync(People entity)
        {
            await _personRepository.AddAsync(entity);
        }
        public async Task UpdateAsync(People entity)
        {
            await _personRepository.UpdateAsync(entity);
        }
        public async Task DeleteAsync(int id)
        {
            await _personRepository.DeleteAsync(id);
        }
        public async Task DeleteAsync(People entity)
        {
            await _personRepository.DeleteAsync(entity);
        }
        public async Task<IEnumerable<People>> FindAsync(Expression<Func<People, bool>> predicate)
        {
            return await _personRepository.FindAsync(predicate);
        }
        public async Task<People?> GetByEmailAsync(string email)
        {
            return await _personRepository.GetByEmailAsync(email);
        }
        public async Task<People?> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _personRepository.GetByPhoneNumberAsync(phoneNumber);
        }
        public async Task<bool> PeopleExistsByIdAsync(int id)
        {
            return await _personRepository.PeopleExistsByIdAsync(id);
        }
    }
}
