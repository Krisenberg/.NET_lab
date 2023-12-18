using List_10.Models;
using Microsoft.EntityFrameworkCore;

namespace List_10.Data
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            Category catBakery = new Category()
            {
                Id = 1,
                Name = "Bakery"
            };
            Category catDairy = new Category() 
            {
                Id = 2,
                Name = "Dairy"
            };

            modelBuilder.Entity<Category>().HasData(
                catBakery,
                catDairy
            );

            modelBuilder.Entity<Article>().HasData(
                new Article()
                {
                    Id = 1,
                    EAN13 = "0123456659934",
                    Name = "Milk",
                    Price = 3.99,
                    ImageFile = null,
                    ImagePath = null,
                    CategoryId = catDairy.Id,
                },
                new Article()
                {
                    Id = 2,
                    EAN13 = "5463987432287",
                    Name = "Bread",
                    Price = 1.25,
                    ImageFile = null,
                    ImagePath = null,
                    CategoryId = catBakery.Id,
                }
            );
        }
    }
}
