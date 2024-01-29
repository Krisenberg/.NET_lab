using List_10.Data;
using List_10.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;

namespace List_10.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>
    {
        private IHostingEnvironment _hostingEnvironment;
        public List<string> CookieIds { get; private set; }
        public CategoryRepository(ShopDbContext context, IHostingEnvironment hostingEnvironment) : base(context) { _hostingEnvironment = hostingEnvironment; }
      
        public async override Task<IActionResult> Delete(int id)
        {
            var category = await _dbSet.FindAsync(id);
            if (category == null)
                return NotFound();

            CookieIds = new List<string>();
            var articles = ((ShopDbContext) _context).Articles.Where(a => a.CategoryId == id);
            foreach (var article in articles)
            {
                if (article.ImagePath != null)
                {
                    string absolutePath = Path.Combine(_hostingEnvironment.WebRootPath, article.ImagePath.TrimStart('~', '/'));
                    if (System.IO.File.Exists(absolutePath))
                    {
                        System.IO.File.Delete(absolutePath);
                    }
                }
                CookieIds.Add("Article_" + article.Id.ToString());
                //Response.Cookies.Delete("Article_" + article.Id.ToString());
            }

            _dbSet.Remove(category);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
