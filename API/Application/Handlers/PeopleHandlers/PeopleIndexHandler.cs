using API.Application.Commands.PeopleCommands;
using API.DataAccess;
using API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Handlers.PeopleHandlers
{
   public class PeopleIndexHandler : IRequestHandler<PeopleIndexCommand, IReadOnlyCollection<People>?>
   {
      private readonly APIContext _context;

      public PeopleIndexHandler(APIContext context)
      {
         _context = context;
      }
      public async Task<IReadOnlyCollection<People>?> Handle(PeopleIndexCommand request, CancellationToken cancellationToken)
      {
         if (_context.Peoples != null)
         {
            IQueryable<People> model = _context.Peoples.AsNoTrackingWithIdentityResolution();
            if (string.IsNullOrEmpty(request.Name) == false)
            {
               model = model.Where(c => EF.Functions.Like(c.Name, $"%{request.Name}%"));
            }
            return await model.ToListAsync(cancellationToken);
         }
         return null;
      }
   }
}
