using System.ComponentModel.DataAnnotations;

namespace BlogProject.Models
{
    public class Blog
    {

        [Required]
        [Key]
        public int ArticleId { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public DateTime PublishedDate = DateTime.Now;
    }
}
