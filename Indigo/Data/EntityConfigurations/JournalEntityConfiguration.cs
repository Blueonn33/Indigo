using Indigo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indigo.Data.EntityConfigurations
{
    public class JournalEntityConfiguration : IEntityTypeConfiguration<Journal>
    {
        public void Configure(EntityTypeBuilder<Journal> builder)
        {
            builder.ToTable("Journals");

            builder.HasKey(j => j.Id);
            builder.Property(j => j.Title).IsRequired().HasMaxLength(100);
            builder.Property(j => j.Description).IsRequired().HasMaxLength(700);
            builder.Property(j => j.ImageUrl).IsRequired();
            builder.Property(j => j.CreationDate);

            builder.HasMany(p => p.Publications)
                 .WithOne(p => p.Journal)
                 .HasForeignKey(j => j.JournalId);
        }
    }
}
