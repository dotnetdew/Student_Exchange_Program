using System.ComponentModel.DataAnnotations;

namespace StudentExchange.Wiut.Web.ViewModels;

public class CreateContactDetailsVM
{
    [Required]
    public string Country { get; set; }
    [Required]
    public string MobilePhoneNumber { get; set; }
    public string OtherPhoneNumber { get; set; }
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
    public string NextOfKinOtherPhone { get; set; }
    public string EmailAddress { get; set; }
    public string StudentId { get; set; }
}
