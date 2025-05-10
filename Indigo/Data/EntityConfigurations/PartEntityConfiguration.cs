using Indigo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indigo.Data.EntityConfigurations
{
    public class PartEntityConfiguration : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            builder.ToTable("Parts");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(100);

            builder.HasOne(p => p.Tome)
                .WithMany(p => p.Parts)
                .HasForeignKey(p => p.TomeId);

            builder.HasMany(p => p.Publications)
                .WithOne(p => p.Part)
                .HasForeignKey(p => p.PartId);
        }
    }
}
