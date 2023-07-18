using API.Application.Commands.PeopleCommands;
using API.DataAccess;
using API.Models;
using MediatR;

namespace API.Application.Handlers.PeopleHandlers
{
   public class PeoplePostHandler : IRequestHandler<PeoplePostCommand, People?>
   {
      private readonly APIContext _context;

      public PeoplePostHandler(APIContext context)
      {
         _context = context;
      }

      public async Task<People?> Handle(PeoplePostCommand request, CancellationToken cancellationToken)
      {
         if (_context.Peoples != null)
         {
            People people = new() { Name = request.Name };
            await _context.Peoples.AddAsync(people, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return people;
         }
         return null;
      }
   }
}
