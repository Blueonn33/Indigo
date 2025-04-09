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

        //public DbSet<Category> Categories { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<KeyWord> KeyWords { get; set; }
        public DbSet<Literature> Literatures { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.ApplyConfiguration(new CategoryEntityConfiguration());
            builder.ApplyConfiguration(new JournalEntityConfiguration());
            builder.ApplyConfiguration(new PublicationEntityConfiguration());
            builder.ApplyConfiguration(new KeyWordEntityConfiguration());
            builder.ApplyConfiguration(new LiteratureEntityConfiguration());

            //SeedInitialData(builder);
        }

        //private void SeedInitialData(ModelBuilder builder)
        //{
        //    //Създаване на категории
        //    builder.Entity<Category>().HasData
        //    (
        //        new Category { Id = 1, Name = "Естествени науки"},
        //        new Category { Id = 2, Name = "Технически науки" },
        //        new Category { Id = 3, Name = "Хуманитарни науки" },
        //        new Category { Id = 4, Name = "Социални науки" },
        //        new Category { Id = 5, Name = "Изкуства" },
        //        new Category { Id = 6, Name = "Спорт" },
        //        new Category { Id = 7, Name = "Медицински науки" },
        //        new Category { Id = 8, Name = "Земеделски науки" },
        //        new Category { Id = 9, Name = "Математически науки" }
        //    );
        //}
    }
}