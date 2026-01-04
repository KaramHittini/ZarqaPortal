namespace ZarqaPortal.Web.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using ZarqaPortal.Web.Core.Entities;

/// <summary>
/// The primary database context for the Zarqa Portal application.
/// </summary>
public class ZarqaPortalDbContext : DbContext
{
    public ZarqaPortalDbContext(DbContextOptions<ZarqaPortalDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the courses in the database.
    /// </summary>
    public DbSet<Course> Courses => Set<Course>();

    /// <summary>
    /// Gets or sets the users in the database.
    /// </summary>
    public DbSet<User> Users => Set<User>();

    /// <summary>
    /// Gets or sets the student profiles in the database.
    /// </summary>
    public DbSet<StudentProfile> StudentProfiles => Set<StudentProfile>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure User entity
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.Username).IsUnique();
        });

        // Configure StudentProfile entity
        modelBuilder.Entity<StudentProfile>(entity =>
        {
            entity.HasIndex(e => e.StudentId).IsUnique();
            entity.HasOne(e => e.User)
                  .WithOne()
                  .HasForeignKey<StudentProfile>(e => e.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Course entity
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasIndex(e => e.Name);
        });
    }
}
