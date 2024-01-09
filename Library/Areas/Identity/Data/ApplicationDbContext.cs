using Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Reader> Readers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Configure Identity
        builder.ApplyConfiguration(new ApplicationUserConfiguration());

        // Configure Library Entities
        builder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);

        builder.Entity<Book>()
            .HasOne(b => b.Publisher)
            .WithMany(p => p.Books)
            .HasForeignKey(b => b.PublisherId);

        builder.Entity<Loan>()
            .HasOne(l => l.Book)
            .WithMany(b => b.Loans)
            .HasForeignKey(l => l.BookId);

        builder.Entity<Loan>()
            .HasOne(l => l.Reader)
            .WithMany(r => r.Loans)
            .HasForeignKey(l => l.ReaderId);
    }
}

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
    }
}