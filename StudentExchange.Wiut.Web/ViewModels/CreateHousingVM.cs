using System.ComponentModel.DataAnnotations;

namespace StudentExchange.Wiut.Web.ViewModels;

public class CreateHousingVM
{
    [Required]
    public string WishToApplyForHousingInUniversity { get; set; }
    [Required]
    public string StudentId { get; set; }
}
