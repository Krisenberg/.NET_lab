using List_10.Data;
using List_10.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

namespace List_10.Repositories
{
    //public class ArticleRepository : IArticleRepository
    //{
    //    private readonly Dictionary<int, Article> articles;

    //    public Article this[int id] => articles.ContainsKey(id) ? articles[id] : null;

    //    public IEnumerable<Article> Articles => articles.Values;

    //    public Article AddArticle(Article article)
    //    {
    //        articles[article.Id] = article;
    //        return article;
    //    }

    //    public void DeleteArticle(int id)
    //    {
    //        articles.Remove(id);
    //    }

    //    public IEnumerable<Article> GetNextNArticles(int id, int n)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Article UpdateArticle(Article article)
    //    {
    //        return AddArticle(article);
    //    }
    //}
    public class ArticleRepository : RepositoryBase<Article>
    {
        public ArticleRepository(ShopDbContext context) : base(context) { }
    }
}
