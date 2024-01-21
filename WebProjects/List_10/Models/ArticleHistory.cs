using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace List_10.Models
{
    public class ArticleHistory
    {
        public int Id { get; set; }

        [Required]
        public string EAN13 { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string CateogryName { get; set; }

        public ICollection<OrderArticle> OrderArticles { get; set; }

        public ArticleHistory() { }

        public ArticleHistory (string ean13,  string name, double price, string catName)
        {
            EAN13 = ean13;
            Name = name;
            Price = price;
            CateogryName = catName;
        }
    }
}
