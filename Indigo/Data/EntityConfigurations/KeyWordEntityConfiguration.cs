using Indigo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indigo.Data.EntityConfigurations
{
    public class KeyWordEntityConfiguration : IEntityTypeConfiguration<KeyWord>
    {
        public void Configure(EntityTypeBuilder<KeyWord> builder)
        {
            builder.ToTable("KeyWords");

            builder.HasKey(k => k.Id);
            builder.Property(k => k.Value).IsRequired().HasMaxLength(100);

            builder.HasOne(k => k.Publication)
                .WithMany(k => k.KeyWords)
                .HasForeignKey(k => k.PublicationId);
        }
    }
}
