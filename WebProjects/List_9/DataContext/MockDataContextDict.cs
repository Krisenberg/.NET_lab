using List_09.ViewModels;
using System.Collections.Generic;
using System;
using System.Linq;

namespace List_09.DataContext
{
    public class MockDataContextDict : IDataContext
    {
        Dictionary<int, Article> articles = new Dictionary<int, Article>()
        {
            { 0, new Article(0, "0001112223334", "Milk", 3.99, new DateTime(2023, 12, 16), Category.Dairy) },
            { 1, new Article(1, "1234509876127", "Beef's meat", 24.99, new DateTime(2023, 12, 31), Category.Meat) }
        };

        public void AddArticle(Article article)
        {
            int newArticleId = articles.Keys.Max() + 1;
            article.Id = newArticleId;
            articles[newArticleId] = article;
        }

        public void RemoveArticle(int id)
        {
            if (articles.ContainsKey(id))
            {
                articles.Remove(id);
            }

        }

        public Article GetArticle(int id)
        {
            return articles.GetValueOrDefault(id);
        }

        public List<Article> GetArticles()
        {
            return articles.Values.OrderBy(art => art.Id).ToList();
        }

        public void UpdateArticle(Article updatedArticle)
        {
            if (articles.ContainsKey(updatedArticle.Id))
            {
                articles[updatedArticle.Id] = updatedArticle;
            }
        }

    }
}
