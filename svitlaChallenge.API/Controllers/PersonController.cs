using svitlaChallenge.Application.Persons.Commands;
using svitlaChallenge.Application.Persons.Queries;
using svitlaChallenge.Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using svitlaChallenge.Application.Persons.Commands.Persons;

namespace svitlaChallenge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {

        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/{id}")]
        public async Task<BaseResult> GetPersonById(Guid id)
        {
            return await _mediator.Send(new GetPersonByIdQuery()
            {
               PersonId = id
            });
        }
        
        [HttpGet()]
        public async Task<BaseResult> GetAllPersons()
        {
            return await _mediator.Send(new GetAllPersonsQuery());
        }
        
        [HttpPost()]
        public async Task<BaseResult> AddPerson([FromBody] AddPersonCommand command)
        {
            return await _mediator.Send(new AddPersonQuery()
            {
                Command = command
            });
        }
        
        [HttpPut("/{id}/birth-info")]
        public async Task<BaseResult> AddPerson(Guid id, [FromBody] BirthInfoCommand command)
        {
            return await _mediator.Send(new BirthInfoQuery()
            {
                PersonId = id,
                Command = command
            });
        }
    }
}
