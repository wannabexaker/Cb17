using CB17.Models;
using Microsoft.EntityFrameworkCore;

namespace CB17.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Candidate> Candidates => Set<Candidate>();
    public DbSet<Certification> Certifications => Set<Certification>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Candidate>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            e.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            e.Property(x => x.Email).IsRequired().HasMaxLength(100);
            e.HasIndex(x => x.Email).IsUnique();
        });

        b.Entity<Certification>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Title).IsRequired().HasMaxLength(100);
            e.Property(x => x.Authority).HasMaxLength(100);
            e.HasOne(x => x.Candidate)
             .WithMany(c => c.Certifications!)
             .HasForeignKey(x => x.CandidateId)
             .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
