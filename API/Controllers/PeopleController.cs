using API.Application.Commands.PeopleCommands;
using API.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   [Route("api/[controller]")]
   [ApiController]

   public class PeopleController : ControllerBase
   {
      private IMediator Mediator { get; }

      public PeopleController(IMediator mediator)
      {
         Mediator = mediator;
      }

      [HttpGet]
      public async Task<ActionResult<IReadOnlyCollection<People>?>> GetPeoples([FromQuery] PeopleIndexCommand command)
      {
         return Ok(await Mediator.Send(command));
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<People>> GetPeople(int id)
      {
         var people = await Mediator.Send(new PeopleGetCommand(id));
         if (people == null) return NotFound();
         return Ok(people);
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> PutPeople(int id, PeoplePutCommand command)
      {
         if (id != command.Id)
         {
            return BadRequest();
         }
         try
         {
            await Mediator.Send(command);
         }
         catch (Exception)
         {
            throw;
         }
         return NoContent();
      }

      [HttpPost]
      public async Task<ActionResult<People>> PostPeople(PeoplePostCommand command)
      {
         var people = await Mediator.Send(command);
         return CreatedAtAction(nameof(GetPeople), new { id = people?.Id ?? 0 }, people);
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> DeletePeople(int id)
      {
         try
         {
            await Mediator.Send(new PeopleDeleteCommand(id));
         }
         catch (Exception)
         {
            throw;
         }
         return NoContent();
      }
   }
}
