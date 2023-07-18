using API.Application.Commands.PeopleCommands;
using API.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Handlers.PeopleHandlers
{
   public class PeopleDeleteHandle : IRequestHandler<PeopleDeleteCommand, int>
   {
      private readonly APIContext _context;

      public PeopleDeleteHandle(APIContext context)
      {
         _context = context;
      }

      public async Task<int> Handle(PeopleDeleteCommand request, CancellationToken cancellationToken)
      {
         if (_context.Peoples != null && request.Id.HasValue)
         {
            return await _context
               .Peoples
               .Where(c => c.Id == request.Id.Value)
               .ExecuteDeleteAsync(cancellationToken);
         }
         return 0;
      }
   }
}
