using System;

namespace Web.Models
{
   public class People
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public DateTime CreatedAt { get; set; }
      public decimal Salary { get; set; }
      public bool Status { get; set; }
      public string Observation { get; set; }
   }
}
