using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Models.Mappings;
namespace Web.Contexts
{
   public class DatabaseContext : DbContext
   {
      public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
      {
      }

      public DbSet<People> Peoples { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.ApplyConfiguration(new PeopleMapping());
      }
   }
}
