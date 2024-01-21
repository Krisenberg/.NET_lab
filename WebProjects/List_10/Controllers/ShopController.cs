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
using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace List_10.Controllers
{
    public class ShopController : Controller
    {
        private readonly ShopDbContext _context;
        private CategorySelectionViewModel model;

        public ShopController(ShopDbContext context)
        {
            _context = context;
            model = new CategorySelectionViewModel
            {
                SelectedCategoryId = -1
            };
        }

        public async Task<IActionResult> Menu()
        {
            var categories = await _context.Categories.ToListAsync();
            model.Categories = categories;

            int? selectedCatIdOptional = HttpContext.Session.GetInt32("selectedCategoryId");
            if (selectedCatIdOptional.HasValue && selectedCatIdOptional.Value != model.SelectedCategoryId)
            {
                HttpContext.Session.SetInt32("selectedCategoryId", model.SelectedCategoryId);
            }
            else
            {
                if (!selectedCatIdOptional.HasValue)
                {
                    model.SelectedCategoryId = categories[0].Id;
                    HttpContext.Session.SetInt32("selectedCategoryId", categories[0].Id);
                }
            }
            ViewData["Categories"] = new SelectList(categories, "Id", "Name", model.SelectedCategoryId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ShowProducts(CategorySelectionViewModel model)
        {

            var categories = await _context.Categories.ToListAsync();
            model.Categories = categories;

            int? selectedCatIdOptional = HttpContext.Session.GetInt32("selectedCategoryId");
            if (selectedCatIdOptional.HasValue && model.ItemAddedToCartId.HasValue)
            {
                model.SelectedCategoryId = selectedCatIdOptional.Value;
            }
            else
            {
                if (selectedCatIdOptional.HasValue && selectedCatIdOptional.Value != model.SelectedCategoryId)
                {
                    HttpContext.Session.SetInt32("selectedCategoryId", model.SelectedCategoryId);
                }
                else
                {
                    if (!selectedCatIdOptional.HasValue)
                    {
                        model.SelectedCategoryId = categories[0].Id;
                        HttpContext.Session.SetInt32("selectedCategoryId", categories[0].Id);
                    }
                }
            }

            var articles = await _context.Articles
                .Include(a => a.Category)
                .Where(a => a.CategoryId == model.SelectedCategoryId)
                .ToListAsync();

            model.Articles = articles;

            ViewData["Categories"] = new SelectList(categories, "Id", "Name", model.SelectedCategoryId);

            if (model.ItemAddedToCartId.HasValue)
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(7);
                string key = "Article_" + model.ItemAddedToCartId.Value.ToString();
                if (Request.Cookies[key] != null)
                {
                    int value = Int32.Parse(Request.Cookies[key]) + 1;
                    Response.Cookies.Append(key, value.ToString(), options);
                }
                else
                {
                    Response.Cookies.Append(key, "1", options);
                }
                model.ItemAddedToCartId = null;
            }

            return View("Menu", model);
        }
    }
}
