using Microsoft.EntityFrameworkCore;
using static BookStore.Models.Config;

namespace BookStore.Models
{
    public class ConfigContext
    {
        public class WideWorldImportersDbContext : DbContext
        {
            public WideWorldImportersDbContext(DbContextOptions<WideWorldImportersDbContext> options) : base(options)
            {

            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Apply configurations for entity

                modelBuilder
                    .ApplyConfiguration(new BooksConfiguration());

                base.OnModelCreating(modelBuilder);
            }
            public DbSet<Book> Books { get; set; }
        }
    }
}
