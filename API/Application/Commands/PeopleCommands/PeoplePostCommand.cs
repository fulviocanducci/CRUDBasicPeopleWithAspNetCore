using API.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace API.Application.Commands.PeopleCommands
{
   public class PeoplePostCommand: IRequest<People?>
   {      

      [Required(ErrorMessage = "Name is required")]
      public string Name { get; set; } = string.Empty;
   }
}
