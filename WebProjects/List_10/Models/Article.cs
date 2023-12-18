using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        //[RegularExpression(@"^\d+(\,\d{1,2})?$")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public double Price { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string ImagePath { get; set; }

        [Required]
        public int CategoryId { get; set; }


        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        // Parameterless constructor required by EF Core
        public Article()
        {
        }

        public Article(string ean13, string name, double price, int categoryId)
        {
            EAN13 = ean13;
            Name = name;
            Price = price;
            CategoryId = categoryId;
        }
    }
}
