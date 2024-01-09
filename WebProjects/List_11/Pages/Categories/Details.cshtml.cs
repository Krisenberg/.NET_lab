using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using List_11.Data;
using List_11.Models;

namespace List_11.Pages.Categories
{
    public class DetailsModel : PageModel
    {
        private readonly List_11.Data.ShopDbContext _context;

        public DetailsModel(List_11.Data.ShopDbContext context)
        {
            _context = context;
        }

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
    }
}
