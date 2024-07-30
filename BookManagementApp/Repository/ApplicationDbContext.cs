using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BookManagementApp.Entities;

public class BookDbContext : DbContext
{

    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
    {

    }

    public BookDbContext() { }
    public DbSet<Books> Books { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                                .SetBasePath(Directory.GetCurrentDirectory())
                                                .AddJsonFile("appsettings.json")
                                                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Books>()
        .HasKey(b => b.BookID);

        modelBuilder.Entity<Books>()
            .Property(b => b.BookID)
            .IsRequired();
    }
}