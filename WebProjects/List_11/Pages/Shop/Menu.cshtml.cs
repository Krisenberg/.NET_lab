using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using List_11.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace List_11.Pages.Shop
{
    public class MenuModel : PageModel
    {
        private readonly List_11.Data.ShopDbContext _context;

        public MenuModel(List_11.Data.ShopDbContext context)
        {
            _context = context;
        }

        public IList<Article> Articles { get; set; }

        [BindProperty]
        public int? CategoryId { get; set; }

        public async Task<IActionResult> OnGet()
        {
            //if (CategoryId == null)
            //    Articles = null;
            //else
            //    Articles = await _context.Articles
            //        .Include(a => a.Category)
            //        .Where(a => a.CategoryId == CategoryId)
            //        .ToListAsync();
            //ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            if (TempData.ContainsKey("SelectedCategoryId") && TempData["SelectedCategoryId"] is int selectedCategoryId)
            {
                CategoryId = selectedCategoryId;
                Articles = await _context.Articles
                    .Include(a => a.Category)
                    .Where(a => a.CategoryId == CategoryId)
                    .ToListAsync();
            }
            else
            {
                Articles = null;
            }
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }

        public IActionResult OnPostAsync()
        {
            TempData["SelectedCategoryId"] = CategoryId;
            return RedirectToPage("./Menu");
        }
    }
}
