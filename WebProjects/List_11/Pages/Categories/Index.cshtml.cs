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
    public class IndexModel : PageModel
    {
        private readonly List_11.Data.ShopDbContext _context;

        public IndexModel(List_11.Data.ShopDbContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            Category = await _context.Categories.ToListAsync();
        }
    }
}
