using Microsoft.EntityFrameworkCore;
using svitlaChallenge.Domain.Models;

namespace svitlaChallenge.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Person?> Persons { get; set; }
    public DbSet<PersonVersion> PersonVersions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Define the relationship
        modelBuilder.Entity<Person>()
            .HasMany(p => p.Versions)
            .WithOne(v => v.Person)
            .HasForeignKey(v => v.PersonId);
    }
}