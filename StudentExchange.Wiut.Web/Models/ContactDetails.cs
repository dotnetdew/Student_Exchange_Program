using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentExchange.Wiut.Web.Models;

public class ContactDetails
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Country { get; set; }
    [Required]
    public string MobilePhoneNumber { get; set; }
    public string? OtherPhoneNumber { get; set; }
    [Required]
    public string NextOfKinTitle {  get; set; }
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

    //Relationship
    public string StudentId { get; set; }
    [ForeignKey("StudentId")]
    public Student Student { get; set; }
}
