using MediatR;

namespace API.Application.Commands.PeopleCommands
{
   public class PeopleDeleteCommand : IRequest<int>
   {
      public PeopleDeleteCommand(int? id)
      {
         Id = id;
      }
      public int? Id { get; set; }
   }
}
