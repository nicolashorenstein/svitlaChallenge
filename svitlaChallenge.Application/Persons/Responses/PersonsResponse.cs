using svitlaChallenge.Domain.Models;
using svitlaChallenge.Domain.Results;

namespace svitlaChallenge.Application.Persons.Responses
{
    public class PersonsResponse : BaseResult
    {
       public List<Person> Persons { get; set; } = new List<Person>();
    }
}
