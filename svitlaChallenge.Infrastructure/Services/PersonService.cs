using Microsoft.EntityFrameworkCore;
using svitlaChallenge.Domain.Interfaces;
using svitlaChallenge.Domain.Models;
using svitlaChallenge.Infrastructure.Persistence;

namespace svitlaChallenge.Infrastructure.Services;

public class PersonService : IPersonService
{
    private readonly AppDbContext _context;

    public PersonService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Person?> GetPersonById(Guid id)
    {
        try
        {
            return await _context.Persons.FirstOrDefaultAsync(c => c.Id.Equals(id));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<List<Person?>> GetAllPeople()
    {
        try
        {
            return await _context.Persons.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task AddPerson(Person? person)
    {
        try
        {
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<bool> UpdateBirthInfo(Guid id, DateTime birthDate, string birthLocation)
    {
        try
        {
            var person = await GetPersonById(id);

            if (person == null)
            {
                Console.WriteLine($"Person with ID {id} not found.");
                return false;
            }

            // Create a new version entry before updating
            var version = new PersonVersion
            {
                PersonId = person.Id,
                GivenName = person.GivenName,
                SurName = person.SurName,
                Gender = person.Gender,
                BirthDate = person.BirthDate,
                BirthLocation = person.BirthLocation,
                DeathDate = person.DeathDate,
                DeathLocation = person.DeathLocation,
                UpdatedAt = DateTime.UtcNow
            };

            person.BirthDate = birthDate;
            person.BirthLocation = birthLocation;

            person.Versions.Add(version);

            _context.Update(person);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}