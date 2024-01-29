using List_10.Data;
using List_10.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace List_10.Repositories
{
    public class ArticleRepository : RepositoryBase<Article>
    {
        private IHostingEnvironment _hostingEnvironment;
        public string CookieId { get; private set; }
        public ArticleRepository(ShopDbContext context, IHostingEnvironment hostingEnvironment) : base(context) { _hostingEnvironment = hostingEnvironment; }

        public async override Task<IActionResult> Delete(int id)
        {
            var article = await _dbSet.FindAsync(id);
            if (article == null)
                return NotFound();

            CookieId = "Article_" + article.Id.ToString();

            if (article.ImagePath != null)
            {
                string absolutePath = Path.Combine(_hostingEnvironment.WebRootPath, article.ImagePath.TrimStart('~', '/'));
                if (System.IO.File.Exists(absolutePath))
                {
                    System.IO.File.Delete(absolutePath);
                }
            }

            _dbSet.Remove(article);
            await _context.SaveChangesAsync();
            return Ok();
        }

        public async Task<List<Article>> GetNextNArticles(int categoryId, int lastId, int n)
        {
            return await _dbSet
                .Where(a => a.Id > lastId && a.CategoryId == categoryId)
                .OrderBy(a => a.Id)
                .Take(n)
                .ToListAsync();
        }
    }
}
