using Indigo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indigo.Data.EntityConfigurations
{
    public class TomeEntityConfiguration : IEntityTypeConfiguration<Tome>
    {
        public void Configure(EntityTypeBuilder<Tome> builder)
        {
            builder.ToTable("Tomes");

            builder.HasKey(j => j.Id);
            builder.Property(j => j.Title).IsRequired().HasMaxLength(100);
            builder.Property(j => j.Description).IsRequired().HasMaxLength(100);

            builder.HasMany(p => p.Parts)
                 .WithOne(p => p.Tome)
                 .HasForeignKey(p => p.TomeId);

            builder.HasOne(j => j.Journal)
                .WithMany(j => j.Tomes)
                .HasForeignKey(j => j.JournalId);
        }
    }
}
