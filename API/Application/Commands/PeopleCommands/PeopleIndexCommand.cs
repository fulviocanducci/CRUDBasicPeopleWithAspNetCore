using API.Models;
using MediatR;

namespace API.Application.Commands.PeopleCommands
{
   public class PeopleIndexCommand: IRequest<IReadOnlyCollection<People>?>
   {
      public string? Name { get; set; }
   }
}
