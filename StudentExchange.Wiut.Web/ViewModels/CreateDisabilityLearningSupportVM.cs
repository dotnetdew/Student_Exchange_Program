using System.ComponentModel.DataAnnotations;

namespace StudentExchange.Wiut.Web.ViewModels;

public class CreateDisabilityLearningSupportVM
{
    [Required]
    public string HaveADisablity { get; set; }
    [Required]
    public string StudentId { get; set; }
}
