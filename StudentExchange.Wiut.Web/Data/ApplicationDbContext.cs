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
}
