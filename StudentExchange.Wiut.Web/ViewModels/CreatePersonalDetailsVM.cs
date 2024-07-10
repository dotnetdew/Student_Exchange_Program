using StudentExchange.Wiut.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentExchange.Wiut.Web.ViewModels;

public class CreatePersonalDetailsVM
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string ForeName { get; set; }
    public string? SecondForeName { get; set; }
    public string? ThirdForeName { get; set; }
    [Required]
    public string FamilyName { get; set; }
    public string? PrefferedName { get; set; }
    public string? PreviousFamilyName { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
    [Required]
    public string Gender { get; set; }
    [Required]
    public string UniversityStudentId { get; set; }
    [Required]
    public string CountryOfBirth { get; set; }
    [Required]
    public string Nationality { get; set; }
    [Required]
    public string PassportNumber { get; set; }
    [Required]
    public DateTime DateOfIssue { get; set; }
    [Required]
    public DateTime DateOfExpiry { get; set; }
    [Required]
    public IFormFile PassportFile { get; set; }
    //Relationship
    [Required]
    public string StudentId { get; set; }
}
