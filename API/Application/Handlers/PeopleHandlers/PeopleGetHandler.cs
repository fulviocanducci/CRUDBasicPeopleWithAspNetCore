using API.Application.Commands.PeopleCommands;
using API.DataAccess;
using API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Handlers.PeopleHandlers
{
   public class PeopleGetHandler : IRequestHandler<PeopleGetCommand, People?>
   {
      private readonly APIContext _context;

      public PeopleGetHandler(APIContext context)
      {
         _context = context;
      }

      public async Task<People?> Handle(PeopleGetCommand request, CancellationToken cancellationToken)
      {
         if (_context.Peoples != null && request.Id.HasValue)
         {
            return await _context
               .Peoples
               .AsNoTracking()
               .FirstOrDefaultAsync(c => c.Id == request.Id.Value, cancellationToken: cancellationToken);
         }
         return null;
      }
   }
}
