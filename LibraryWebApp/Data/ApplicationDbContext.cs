using LibraryWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure "one-to-many" relationships
            modelBuilder.Entity<Book>()
                .HasOne(book => book.Library)
                .WithMany(library => library.Books)
                .HasForeignKey(book => book.LibraryId);

            modelBuilder.Entity<Reader>()
                .HasOne(reader => reader.Library)
                .WithMany(library => library.Readers)
                .HasForeignKey(reader => reader.LibraryId);
        }
    }
}
