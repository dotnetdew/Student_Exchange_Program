using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentExchange.Wiut.Web.Models;

namespace StudentExchange.Wiut.Web.Data;

public class ApplicationDbContext : IdentityDbContext<Student>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<PersonalDetails> PersonalDetails { get; set; }
    public DbSet<ContactDetails> ContactDetails { get; set; }
    public DbSet<EducationalDetails> EducationalDetails { get; set; }
    public DbSet<DisabilityLearningSupport> DisabilityLearningSupports { get; set; }
    public DbSet<Housing> Housings { get; set; }
    public DbSet<Submission> Submissions { get; set; }
    public DbSet<IdentityRole> Roles {  get; set; }
    public DbSet<FileDetail> FileDetails { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>()
            .HasOne(s => s.PersonalDetails)
            .WithOne(p => p.Student)
            .HasForeignKey<PersonalDetails>(p => p.StudentId);

        modelBuilder.Entity<Student>()
            .HasOne(s => s.ContactDetails)
            .WithOne(c => c.Student)
            .HasForeignKey<ContactDetails>(c => c.StudentId);

        modelBuilder.Entity<Student>()
            .HasOne(s => s.EducationalDetails)
            .WithOne(e => e.Student)
            .HasForeignKey<EducationalDetails>(e => e.StudentId);

        modelBuilder.Entity<Student>()
            .HasOne(s => s.DisabilityLearningSupport)
            .WithOne(d => d.Student)
            .HasForeignKey<DisabilityLearningSupport>(d => d.StudentId);

        modelBuilder.Entity<Student>()
            .HasOne(s => s.Housing)
            .WithOne(h => h.Student)
            .HasForeignKey<Housing>(h => h.StudentId);

        modelBuilder.Entity<Submission>()
            .HasOne(s => s.Student)
            .WithOne(s => s.Submission)
            .HasForeignKey<Submission>(s => s.StudentId)
            .IsRequired();
    }
}
