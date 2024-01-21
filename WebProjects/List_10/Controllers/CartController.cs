using List_10.Data;
using List_10.Models;
using List_10.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace List_10.Controllers
{
    [Authorize(Policy = "NotAdmin")]
    public class CartController : Controller
    {
        private readonly ShopDbContext _context;
        //private CartViewModel model;
        //private OrderViewModel orderVM;
        private CompoundViewModel model;

        public CartController(ShopDbContext context)
        {
            _context = context;
            //model = new CartViewModel();
            //orderVM = new OrderViewModel();
            model = new CompoundViewModel();
            model.CartVM = new CartViewModel();
            model.OrderVM = new OrderViewModel();
        }

        public async Task<IActionResult> Index()
        {
            var shopArticles = await _context.Articles.ToListAsync();
            var cartItems = new List<CartItem>();
            double totalValue = 0;

            foreach (var article in shopArticles)
            {
                string quantity = Request.Cookies["Article_" + article.Id.ToString()];
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
            model.CartVM.cartItems = cartItems;
            model.CartVM.cartValue = totalValue;

            return View(model);
        }

        public IActionResult DecreaseQuantity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string quantity = Request.Cookies["Article_" + id.Value.ToString()];
            if (quantity != null)
            {
                int newQuant = Int32.Parse(quantity) - 1;
                if (newQuant == 0)
                    Response.Cookies.Delete("Article_" + id.Value.ToString());
                else
                {
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Append("Article_" + id.Value.ToString(), newQuant.ToString(), options);
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
            string quantity = Request.Cookies["Article_" + id.Value.ToString()];
            if (quantity != null)
            {
                int newQuant = Int32.Parse(quantity) + 1;
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Append("Article_" + id.Value.ToString(), newQuant.ToString(), options);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Response.Cookies.Delete("Article_" + id.Value.ToString());

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Summary(CompoundViewModel compoundVM)
        {
            var shopArticles = await _context.Articles.ToListAsync();
            var cartItems = new List<CartItem>();
            double totalValue = 0;

            foreach (var article in shopArticles)
            {
                string quantity = Request.Cookies["Article_" + article.Id.ToString()];
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
            if (compoundVM != null && compoundVM.CartVM != null && compoundVM.OrderVM != null)
            {
                model = compoundVM;
            }
            model.CartVM.cartItems = cartItems;
            model.CartVM.cartValue = totalValue;

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ShowOrder(CompoundViewModel compoundVM)
        {
            if (ModelState.IsValid)
            {
                bool isPaid = (compoundVM.OrderVM.PaymentMethod.ToLower() == PaymentMethods.Cash_on_delivery.ToString().ToLower().Replace('_', ' ')) ? false : true;
                var order = new Order(
                        compoundVM.OrderVM.UserEmail,
                        compoundVM.OrderVM.FirstName,
                        compoundVM.OrderVM.LastName,
                        compoundVM.OrderVM.Email,
                        compoundVM.OrderVM.Street,
                        compoundVM.OrderVM.AddressLine1,
                        compoundVM.OrderVM.AddressLine2,
                        compoundVM.OrderVM.City,
                        compoundVM.OrderVM.Country,
                        compoundVM.OrderVM.Zip,
                        compoundVM.OrderVM.PaymentMethod,
                        isPaid
                    );
                _context.Add(order);
                await _context.SaveChangesAsync();

                var orderId = order.Id;

                var cookies = HttpContext.Request.Cookies;
                var articlesQuantities = cookies.Keys
                    .Where(key => key.StartsWith("Article_", StringComparison.OrdinalIgnoreCase))
                    .ToDictionary(key => Int32.Parse(key.Substring(8, key.Length - 8)), key => Int32.Parse(cookies[key]));

                foreach (var artQuant in articlesQuantities)
                {
                    int artID = artQuant.Key;
                    int quantity = artQuant.Value;

                    Article article = _context.Articles
                            .Include(a => a.Category)
                            .FirstOrDefault(a => a.Id == artID);

                    ArticleHistory artHist = _context.FindHistArticleWithSameAttributes(
                            ean13: article.EAN13,
                            artName: article.Name,
                            price: article.Price,
                            catName: article.Category.Name
                        );

                    var artHistId = 0;

                    if (artHist != null)
                    {
                        artHistId = artHist.Id;
                    }
                    else
                    {
                        ArticleHistory articleHistory = new ArticleHistory(
                                article.EAN13,
                                article.Name,
                                article.Price,
                                article.Category.Name
                            );

                        _context.Add(articleHistory);
                        await _context.SaveChangesAsync();
                        artHistId = articleHistory.Id;
                    }
                    var artSubtotalValue = quantity * article.Price;
                    OrderArticle orderArticle = new OrderArticle(
                            artHistId,
                            quantity,
                            artSubtotalValue,
                            orderId
                        );
                    _context.Add(orderArticle);
                    await _context.SaveChangesAsync();
                    Response.Cookies.Delete("Article_" + artID.ToString());
                }

                return View(compoundVM);
            }
            return RedirectToAction("Summary", compoundVM);
        }
    }
}
