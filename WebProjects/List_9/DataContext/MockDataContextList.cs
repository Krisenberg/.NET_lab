using List_09.ViewModels;
using System.Collections.Generic;
using System;
using System.Linq;

namespace List_09.DataContext
{
    public class MockDataContextList : IDataContext
    {
        List<Article> articles = new List<Article>()
        {
            new Article(0, "0001112223334", "Milk", 3.99, new DateTime(2023,12,16), Category.Dairy),
            new Article(1, "1234509876127", "Beef's meat", 24.99, new DateTime(2023,12,31), Category.Meat)
        };
        public void AddArticle(Article article)
        {
            int newArticleId = articles.Max( article => article.Id ) + 1;
            article.Id = newArticleId;
            articles.Add(article);
        }

        public void RemoveArticle(int id)
        {
            Article articleToDelete = articles.FirstOrDefault(article => article.Id == id);
            if (articleToDelete != null)
            {
                articles.Remove(articleToDelete);
            }

        }

        public Article GetArticle(int id)
        {
            return articles.FirstOrDefault(article => article.Id == id);
        }

        public List<Article> GetArticles()
        {
            return articles;
        }

        public void UpdateArticle(Article updatedArticle)
        { 
            articles = articles.Select(art => (art.Id == updatedArticle.Id) ? updatedArticle : art).ToList();
        }
    }
}
