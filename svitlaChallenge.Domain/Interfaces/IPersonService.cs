using svitlaChallenge.Domain.Models;

namespace svitlaChallenge.Domain.Interfaces
{
    public interface IPersonService
    {
        public Task<Person?> GetPersonById(Guid id);
        public Task<List<Person>> GetAllPeople();
        public Task AddPerson(Person? person);
        public Task<bool> UpdateBirthInfo(Guid id, DateTime birthDate, string birthLocation);
    }
}
