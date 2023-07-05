using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Models.Mappings
{
   public class PeopleMapping : IEntityTypeConfiguration<People>
   {
      public void Configure(EntityTypeBuilder<People> builder)
      {
         builder.ToTable("Peoples");
         builder.HasKey(x => x.Id);
         builder.Property(x => x.Id).HasColumnName("Id").UseIdentityColumn();
         builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(100).UseCollation("Latin1_General_CI_AI").IsRequired();
         builder.Property(x => x.CreatedAt).HasColumnName("CreatedAt").IsRequired();
         builder.Property(x => x.Salary).HasColumnName("Salary").HasPrecision(18, 2).IsRequired();
         builder.Property(x => x.Status).HasColumnName("Status").IsRequired();
         builder.Property(x => x.Observation).HasColumnName("Observation");
      }
   }
}
