using MediatR;
using System.ComponentModel.DataAnnotations;

namespace API.Application.Commands.PeopleCommands
{
   public class PeoplePutCommand: IRequest
   {
      [Required(ErrorMessage = "Id is required")]
      public int Id { get; set; }

      [Required(ErrorMessage = "Name is required")]
      public string Name { get; set; } = string.Empty;
   }
}
