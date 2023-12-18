using List_10.Models;
using Microsoft.EntityFrameworkCore;

namespace List_10.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasOne(a => a.Category)                  // Article has one Category
                .WithMany(c => c.Articles)                // Category has many Articles
                .HasForeignKey(a => a.CategoryId);        // Define the foreign key relationship

            // Optionally, if you want to configure the relationship in Category entity too
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Articles)                 // Category has many Articles
                .WithOne(a => a.Category)                 // Article has one Category
                .HasForeignKey(a => a.CategoryId);

            modelBuilder.Seed();
        }
    }
}
