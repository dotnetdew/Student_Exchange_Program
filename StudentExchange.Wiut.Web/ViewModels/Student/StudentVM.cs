using System.ComponentModel.DataAnnotations;

namespace StudentExchange.Wiut.Web.ViewModels.Student;

public class StudentVM
{
    [Required]
    public string Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string ForeName { get; set; }
    [Required]
    public string FamilyName { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Status { get; set; }
}
