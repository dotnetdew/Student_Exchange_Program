using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentExchange.Wiut.Web.Models;

public class Submission
{
    [Key]
    public int Id { get; set; }

    public bool AgreeToStatements { get; set; }

    public DateTime SubmissionCreated { get; set; }

    [Required]
    public string StudentId { get; set; }

    [ForeignKey("StudentId")]
    public virtual Student Student { get; set; }
}
