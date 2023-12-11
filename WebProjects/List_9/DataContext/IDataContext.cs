using List_09.ViewModels;
using System.Collections.Generic;

namespace List_09.DataContext
{
    public interface IDataContext
    {
        List<Article> GetArticles();
        Article GetArticle(int id);
        void AddArticle(Article article);
        void RemoveArticle(int id);
        void UpdateArticle(Article article);
    }
}
