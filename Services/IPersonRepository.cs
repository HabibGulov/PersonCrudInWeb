public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetAllPersonsAsync();
    Task<Person?> GetPersonByIdAsync(Guid id);
    Task<bool> CreatePersonAsync(Person person);
    Task<bool> UpdatePersonAsync(Person person);
    Task<bool> DeletePersonAsync(Guid id);
}