using Indigo.Data.EntityConfigurations;
using Indigo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Indigo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Journal> Journals { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<KeyWord> KeyWords { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new JournalEntityConfiguration());
            builder.ApplyConfiguration(new PublicationEntityConfiguration());
            builder.ApplyConfiguration(new KeyWordEntityConfiguration());
        }
    }
}