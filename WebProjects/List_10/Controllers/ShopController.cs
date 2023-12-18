using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using List_10.Data;
using List_10.Models;
using List_10.ViewModels;

namespace List_10.Controllers
{
    public class ShopController : Controller
    {
        private readonly ShopDbContext _context;

        public ShopController(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Menu()
        {
            var categories = await _context.Categories.ToListAsync();
            var model = new CategorySelectionViewModel
            {
                Categories = categories,
                SelectedCategoryId = categories[0].Id
            };
            ViewData["Categories"] = new SelectList(categories, "Id", "Name", model.SelectedCategoryId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ShowProducts(CategorySelectionViewModel model)
        {

            var categories = await _context.Categories.ToListAsync();

            var articles = await _context.Articles
                .Include(a => a.Category)
                .Where(a => a.CategoryId == model.SelectedCategoryId)
                .ToListAsync();

            model.Articles = articles;

            ViewData["Categories"] = new SelectList(categories, "Id", "Name", model.SelectedCategoryId);

            return View("Menu", model);
        }
    }
}
