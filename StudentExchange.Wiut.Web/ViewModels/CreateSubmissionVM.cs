using System.ComponentModel.DataAnnotations;

namespace StudentExchange.Wiut.Web.ViewModels
{
    public class CreateSubmissionVM
    {
        [Required]
        public bool AgreeToStatements { get; set; }
        [Required]
        public DateTime SubmissionCreated { get; set; }
        [Required]
        public string StudentId { get; set; }
        public string EmailAddress { get; set; }
    }
}
