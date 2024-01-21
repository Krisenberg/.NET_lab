using List_10.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace List_10.Data
{
    public class ShopDbContext : IdentityDbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ArticleHistory> ArticlesHistorical { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderArticle> OrdersArticles { get; set; }

        public ArticleHistory FindHistArticleWithSameAttributes(string ean13, string artName, double price, string catName)
        {
            var article = ArticlesHistorical
                .FirstOrDefault(a =>
                    a.EAN13 == ean13 &&
                    a.Name == artName &&
                    a.Price == price &&
                    a.CateogryName == catName
                );
            return article;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Article>()
                .HasOne(a => a.Category)
                .WithMany(c => c.Articles)
                .HasForeignKey(a => a.CategoryId);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Articles)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.CategoryId);

            modelBuilder.Entity<ArticleHistory>()
                .HasMany(ah => ah.OrderArticles)
                .WithOne(oa => oa.ArticleHistory)
                .HasForeignKey(oa => oa.ArticleHistoryId);

            modelBuilder.Entity<OrderArticle>()
                .HasOne(oa => oa.ArticleHistory)
                .WithMany(ah => ah.OrderArticles)
                .HasForeignKey(oa => oa.ArticleHistoryId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderArticles)
                .WithOne(oa => oa.Order)
                .HasForeignKey(oa => oa.OrderId);

            modelBuilder.Entity<OrderArticle>()
                .HasOne(oa => oa.Order)
                .WithMany(o => o.OrderArticles)
                .HasForeignKey(oa => oa.OrderId);

            modelBuilder.Seed();
        }
    }
}
