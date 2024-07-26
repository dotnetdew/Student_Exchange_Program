using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentExchange.Wiut.Web.Models
{
    public class FileDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string FileType { get; set; }
        [Required]
        public byte[] Content { get; set; }

        // Relationship
        public int PersonalDetailsId { get; set; }
        [ForeignKey("PersonalDetailsId")]
        public virtual PersonalDetails PersonalDetails { get; set; }
    }
}
