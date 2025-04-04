using Indigo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indigo.Data.EntityConfigurations
{
    public class LiteratureEntityConfiguration : IEntityTypeConfiguration<Literature>
    {
        public void Configure(EntityTypeBuilder<Literature> builder) 
        {
            builder.ToTable("Literatures");

            builder.HasKey(l => l.Id);
            builder.Property(l => l.Name).IsRequired().HasMaxLength(100);
            builder.Property(l => l.Content).IsRequired().HasMaxLength(200);

            builder.HasOne(l => l.Publication)
                   .WithMany(l => l.Literatures)
                   .HasForeignKey(l => l.PublicationId);
        }
    }
}
