using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Models
{
    public class Config
    {
        public class BooksConfiguration : IEntityTypeConfiguration<Book>
        {
            public void Configure(EntityTypeBuilder<Book> builder)
            {
                // Set configuration for entity
                builder.ToTable("Book");
                // Set key for entity
                builder.HasKey(p => p.BookID);
                builder.Property(p => p.BookName).HasColumnType("nvarchar(200)").IsRequired();
                builder.Property(p => p.Price).HasColumnType("float").IsRequired();
                builder.Property(p => p.Category).HasColumnType("varchar(30)").IsRequired();
                builder.Property(p => p.Author).HasColumnType("varchar(30)").IsRequired();
            }

            public void ConfigureDataMaster(EntityTypeBuilder<DataMaster> builder)
            {
                builder.ToTable("DataMaster");
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Name).HasColumnName("nvarchar(200)").IsRequired();
            }

        }
    }
}
