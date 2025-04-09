using Indigo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Indigo.Data.EntityConfigurations
{
    public class PublicationEntityConfiguration : IEntityTypeConfiguration<Publication>
    {
        public void Configure(EntityTypeBuilder<Publication> builder)
        {
            builder.ToTable("Publications");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Topic).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(700);
            builder.Property(p => p.Content).IsRequired().HasMaxLength(700);
            builder.Property(p => p.AuthorName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.CreationDate);
            builder.Property(p => p.IsApproved);

            builder.HasOne(p => p.Journal)
                .WithMany(p => p.Publications)
                .HasForeignKey(p => p.JournalId);
        }
    }
}
