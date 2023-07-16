using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace API.Models.Mappings
{
   public class PeopleMapping : IEntityTypeConfiguration<People>
   {
      public void Configure(EntityTypeBuilder<People> builder)
      {
         builder.ToTable("peoples");

         builder.HasKey(x => x.Id);
         builder.Property(x => x.Id).HasColumnName("id").UseMySqlIdentityColumn();

         builder.Property(x => x.Name)
            .HasColumnName("name")
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasMaxLength(100)
            .IsRequired();
      }
   }
}