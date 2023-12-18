using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace List_09.ViewModels
{
    public enum Category { Beverage, Bakery, Dairy, Meat, Produce, Other }
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
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public double Price { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        [Display(Name="Expiry date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = false)]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public Category Category { get; set; }

        public Article() { }

        public Article(int id, string eAN13, string name, double price, DateTime expirationDate, Category category)
        {
            Id = id;
            EAN13 = eAN13;
            Name = name;
            Price = price;
            ExpirationDate = expirationDate;
            Category = category;
        }
    }
}
