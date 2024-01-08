using List_10.Data;
using List_10.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace List_10.Controllers
{

    public class CartController : Controller
    {
        private readonly ShopDbContext _context;
        private CartViewModel model;

        public CartController(ShopDbContext context)
        {
            _context = context;
            model = new CartViewModel();
        }

        public async Task<IActionResult> Index()
        {
            var shopArticles = await _context.Articles.ToListAsync();
            var cartItems = new List<CartItem>();
            double totalValue = 0;

            foreach (var article in shopArticles)
            {
                string quantity = Request.Cookies[article.Id.ToString()];
                if (quantity != null)
                {
                    var art = _context.Articles
                        .Include(a => a.Category)
                        .FirstOrDefault(a => a.Id == article.Id);
                    string catName = art.Category.Name;
                    CartItem item = new CartItem(article, catName, Int32.Parse(quantity));
                    cartItems.Add(item);
                    totalValue += (item.Quantity * item.Price);
                }
            }
            model.cartItems = cartItems;
            model.cartValue = totalValue;

            return View(model);
        }

        public IActionResult DecreaseQuantity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string quantity = Request.Cookies[id.Value.ToString()];
            if (quantity != null)
            {
                int newQuant = Int32.Parse(quantity) - 1;
                if (newQuant == 0)
                    Response.Cookies.Delete(id.Value.ToString());
                else
                {
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Append(id.Value.ToString(), newQuant.ToString(), options);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult IncreaseQuantity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string quantity = Request.Cookies[id.Value.ToString()];
            if (quantity != null)
            {
                int newQuant = Int32.Parse(quantity) + 1;
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Append(id.Value.ToString(), newQuant.ToString(), options);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Response.Cookies.Delete(id.Value.ToString());

            return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        //public async Task<IActionResult> DecreaseQuantity(CartViewModel model)
        //{
        //    var shopArticles = await _context.Articles.ToListAsync();
        //    var cartArticles = new List<Models.Article>();
        //    var articlesQuantities = new Dictionary<int, int>();

        //    foreach (var article in shopArticles)
        //    {
        //        string quantity = Request.Cookies[article.Id.ToString()];
        //        if (quantity != null)
        //        {
        //            cartArticles.Add(article);
        //            articlesQuantities.Add(article.Id, Int32.Parse(quantity));
        //        }
        //    }

        //    model.Articles = cartArticles;
        //    model.ArticlesQuantities = articlesQuantities;
        //    if (model.IdDecreaseItemQuantity.HasValue && model.ArticlesQuantities[model.IdDecreaseItemQuantity.Value] > 1)
        //    {
        //        int newQuant = model.ArticlesQuantities[model.IdDecreaseItemQuantity.Value] - 1;
        //        model.ArticlesQuantities[model.IdDecreaseItemQuantity.Value] = newQuant;
        //        CookieOptions options = new CookieOptions();
        //        options.Expires = DateTime.Now.AddDays(7);
        //        Response.Cookies.Append(model.IdDecreaseItemQuantity.Value.ToString(), newQuant.ToString(), options);
        //    }
        //    if (model.IdDecreaseItemQuantity.HasValue && model.ArticlesQuantities[model.IdDecreaseItemQuantity.Value] == 1)
        //    {
        //        int newQuant = model.ArticlesQuantities[model.IdDecreaseItemQuantity.Value] - 1;
        //        model.ArticlesQuantities.Remove(model.IdDecreaseItemQuantity.Value);
        //        Response.Cookies.Delete(model.IdDecreaseItemQuantity.Value.ToString());

        //        var article = await _context.Articles
        //            .Include(a => a.Category)
        //            .Where(a => a.CategoryId == model.IdDecreaseItemQuantity.Value)
        //            .ToListAsync();

        //        model.Articles.Remove(article.FirstOrDefault());
        //    }

        //    model.IdDecreaseItemQuantity = null;
        //    return RedirectToAction(nameof(Index));
        //}

        //[HttpPost]
        //public async Task<IActionResult> IncreaseQuantity(CartViewModel model)
        //{
        //    var shopArticles = await _context.Articles.ToListAsync();
        //    var cartArticles = new List<Models.Article>();
        //    var articlesQuantities = new Dictionary<int, int>();

        //    foreach (var article in shopArticles)
        //    {
        //        string quantity = Request.Cookies[article.Id.ToString()];
        //        if (quantity != null)
        //        {
        //            cartArticles.Add(article);
        //            articlesQuantities.Add(article.Id, Int32.Parse(quantity));
        //        }
        //    }

        //    model.Articles = cartArticles;
        //    model.ArticlesQuantities = articlesQuantities;
        //    if (model.IdIncreaseItemQuantity.HasValue)
        //    {
        //        int newQuant = model.ArticlesQuantities[model.IdIncreaseItemQuantity.Value] + 1;
        //        model.ArticlesQuantities[model.IdIncreaseItemQuantity.Value] = newQuant;
        //        CookieOptions options = new CookieOptions();
        //        options.Expires = DateTime.Now.AddDays(7);
        //        Response.Cookies.Append(model.IdIncreaseItemQuantity.Value.ToString(), newQuant.ToString(), options);
        //    }
        //    model.IdIncreaseItemQuantity = null;
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
