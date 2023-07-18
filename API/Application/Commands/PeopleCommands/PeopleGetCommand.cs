using API.Models;
using MediatR;

namespace API.Application.Commands.PeopleCommands
{
   public class PeopleGetCommand: IRequest<People?>
   {
      public int? Id { get; set; }

      public PeopleGetCommand(int? id)
      {
         Id = id;
      }
   }
}
