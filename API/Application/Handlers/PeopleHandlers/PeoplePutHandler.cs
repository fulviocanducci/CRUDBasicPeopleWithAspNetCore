using API.Application.Commands.PeopleCommands;
using API.DataAccess;
using MediatR;

namespace API.Application.Handlers.PeopleHandlers
{
   public class PeoplePutHandler : IRequestHandler<PeoplePutCommand>
   {
      private readonly APIContext _context;

      public PeoplePutHandler(APIContext context)
      {
         _context = context;
      }

      public async Task Handle(PeoplePutCommand request, CancellationToken cancellationToken)
      {
         if (_context.Peoples != null)
         {
            var people = await _context.Peoples.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);
            if (people != null && request.Id == people.Id)
            {
               people.Name = request.Name;
               await _context.SaveChangesAsync(cancellationToken);
            }
         }
         await Task.CompletedTask;
      }
   }
}
