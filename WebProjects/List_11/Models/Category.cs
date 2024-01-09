using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace List_11.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Category() { }

        public Category(string name)
        {
            Name = name;
        }

        public ICollection<Article> Articles { get; set; }
    }
}
