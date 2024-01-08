using System.Collections.Generic;
using List_10.Models;

namespace List_10.ViewModels
{
    public class CategorySelectionViewModel
    {
        public int SelectedCategoryId { get; set; }
        public List<Category> Categories { get; set; }
        public List<Article> Articles { get; set; }
        public int? ItemAddedToCartId { get; set; }
    }
}
