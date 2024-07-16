using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace StudentExchange.Wiut.Web.Models
{
    public class Student : IdentityUser
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string ForeName { get; set; }
        public string? ForeName2 { get; set; }
        public string? ForeName3 { get; set; }
        [Required]
        public string FamilyName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }

        public virtual PersonalDetails PersonalDetails { get; set; }
        public virtual ContactDetails ContactDetails { get; set; }
        public virtual EducationalDetails EducationalDetails { get; set; }
        public virtual DisabilityLearningSupport DisabilityLearningSupport { get; set; }
        public virtual Housing Housing { get; set; }
        public virtual Submission Submission { get; set; }
    }
}
