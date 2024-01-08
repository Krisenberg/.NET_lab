using List_10.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace List_10.ViewModels
{
    public struct CartItem
    {
        public int Id { get; }
        public string EAN13 { get; }
        public string Name { get; }
        public double Price { get; }
        [Display(Name = "Image")]
        public string ImagePath { get; }
        [Display(Name = "Category")]
        public string CategoryName { get; }
        public int Quantity { get; }

        public CartItem(Models.Article article, string catName, int quant)
        {
            Id = article.Id;
            EAN13 = article.EAN13;
            Name = article.Name;
            Price = article.Price;
            ImagePath = article.ImagePath;
            CategoryName = catName;
            Quantity = quant;
        }
    }

    public class CartViewModel
    {
        public List<CartItem> cartItems { get; set; }
        public double cartValue { get; set; }
    }
}
