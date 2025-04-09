using Indigo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indigo.Data.EntityConfigurations
{
    //public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    //{
    //    public void Configure(EntityTypeBuilder<Category> builder)
    //    {
    //        builder.ToTable("Categories");
    //        builder.HasKey(c => c.Id);
    //        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

    //        builder.HasMany(c => c.Journals)
    //            .WithOne(c => c.Category)
    //            .HasForeignKey(j => j.CategoryId);
    //    }
    //}
}
