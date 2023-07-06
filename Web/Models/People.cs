using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Models.Validations;
namespace Web.Models
{
   public class People
   {
      public int Id { get; set; }

      [Required(ErrorMessage = "Digite o nome")]
      [DisplayName("Nome Completo")]
      public string Name { get; set; }


      [DisplayName("Data")]
      [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
      [Required(ErrorMessage = "Digite a data")]
      [RequiredDateTime(ErrorMessage = "Data inválida")]
      public DateTime CreatedAt { get; set; }

      [DisplayName("Salário")]
      [DisplayFormat(DataFormatString = "{0:N2}")]
      [Required(ErrorMessage = "Digite o salário")]
      [RequiredMoney(1, ErrorMessage = "Valor inválido")]
      public decimal Salary { get; set; }

      [DisplayName("Status")]
      public bool Status { get; set; }

      [DisplayName("Observações")]
      public string Observation { get; set; }
   }
}
