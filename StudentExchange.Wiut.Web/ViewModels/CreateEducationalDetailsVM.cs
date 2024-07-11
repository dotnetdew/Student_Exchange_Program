using System.ComponentModel.DataAnnotations;

namespace StudentExchange.Wiut.Web.ViewModels;

public class CreateEducationalDetailsVM
{
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
    [Required]
    public string StudentId { get; set; }
}
