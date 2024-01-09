using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using List_11.Data;
using List_11.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace List_11.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly List_11.Data.ShopDbContext _context;
        private IHostingEnvironment _hostingEnvironment;

        public DeleteModel(List_11.Data.ShopDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public Category Category { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Id == null)
            {
                return NotFound();
            }

            Category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == Id);

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Id == null)
            {
                return NotFound();
            }

            Category = await _context.Categories.FindAsync(Id);

            if (Category != null)
            {
                var articles = _context.Articles.Where(a => a.CategoryId == Id);
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
                }
                _context.Categories.Remove(Category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
