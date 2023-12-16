using System.ComponentModel.DataAnnotations;

namespace List_10.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{13}$")]
        public string EAN13 { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Article's name must be at least 3 characters long")]
        [MaxLength(20, ErrorMessage = "Article's name must be at most 20 characters long")]
        public string Name { get; set; }
    }
}
