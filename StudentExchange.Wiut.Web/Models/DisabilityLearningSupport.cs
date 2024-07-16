using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentExchange.Wiut.Web.Models;

public class DisabilityLearningSupport
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string HaveADisablity { get; set; }

    //Relationship
    public string StudentId { get; set; }
    [ForeignKey("StudentId")]
    public virtual Student Student { get; set; }
}
