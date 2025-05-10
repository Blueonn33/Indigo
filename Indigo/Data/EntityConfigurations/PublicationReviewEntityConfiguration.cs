using Indigo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indigo.Data.EntityConfigurations
{
    public class PublicationReviewEntityConfiguration : IEntityTypeConfiguration<PublicationReview>
    {
        public void Configure(EntityTypeBuilder<PublicationReview> builder)
        {
            builder.ToTable("PublicationReviews");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Title).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Comment).IsRequired().HasMaxLength(1000);
            builder.Property(r => r.ReviewerName).IsRequired().HasMaxLength(100);
            builder.Property(r => r.ReviewerEmail).IsRequired().HasMaxLength(100);
            builder.Property(r => r.ReviewDate);

            builder.HasOne(r => r.Publication)
                .WithMany(p => p.PublicationReviews)
                .HasForeignKey(r => r.PublicationId);
        }
    }
}
