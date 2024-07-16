using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentExchange.Wiut.Web.Models;

public class EducationalDetails
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string ExchangePartnerInstitution { get; set; }
    [Required]
    public string ExactNameOfDegreeProgramme { get; set; }
    [Required]
    public string TotalLengthOfDegreeProgrammeInYears { get; set; }
    [Required]
    public string ExpectedMonthOfGraduation { get; set; }
    [Required]
    public string ExpectedYearOfGraduation { get; set; }
    [Required]
    public string IsEnglishFirstLanguage { get; set; }
    //Relationship
    public string StudentId { get; set; }
    [ForeignKey("StudentId")]
    public virtual Student Student { get; set; }
}
