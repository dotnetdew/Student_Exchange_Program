using System.ComponentModel.DataAnnotations;

namespace StudentExchange.Wiut.Web.ViewModels.Student;

public class StudentDetailsVM
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

    //////////////////////////////////////////////
    public string? SecondForeName { get; set; }
    public string? ThirdForeName { get; set; }
    public string? PrefferedName { get; set; }
    public string? PreviousFamilyName { get; set; }
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
    public byte[] PassportFile { get; set; }
    [Required]
    public string PassportFileName { get; set; }
    [Required]
    public string PassportFileType { get; set; }
    ///////////////////////////////////////////
    [Required]
    public string Country { get; set; }
    [Required]
    public string MobilePhoneNumber { get; set; }
    public string? OtherPhoneNumber { get; set; }
    [Required]
    public string NextOfKinTitle { get; set; }
    public string NextOfKinForeName { get; set; }
    [Required]
    public string NextOfKinSurName { get; set; }
    [Required]
    public string Relationship { get; set; }
    [Required]
    public string NextOfKinCountry { get; set; }
    [Required]
    public string NextOfKinMobilePhone { get; set; }
    public string? NextOfKinOtherPhone { get; set; }
    public string? EmailAddress { get; set; }
    ////////////////////////////////////////////////////////////
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
    //////////////////////////////////////////////////////////

    [Required]
    public string HaveADisablity { get; set; }

    //////////////////////////////////////////////////////////
    [Required]
    public string WishToApplyForHousingInUniversity { get; set; }

    public string AgreeToStatements { get; set; }

    public DateTime SubmissionCreated { get; set; }

}
