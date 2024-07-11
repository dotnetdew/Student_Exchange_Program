using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentExchange.Wiut.Web.Models;

public class Housing
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string WishToApplyForHousingInUniversity {  get; set; }
    public string StudentId { get; set; }
    [ForeignKey("StudentId")]
    public Student Student { get; set; }
}
