namespace API.Models
{
   public class People
   {
      public People()
      {
         Id = 0;
         Name = string.Empty;
      }

      public People(string name)
      {
         Name = name;
      }

      public People(int id, string name)
      {
         Id = id;
         Name = name;
      }

      public int Id { get; set; }
      public string Name { get; set; } = null!;
   }
}
