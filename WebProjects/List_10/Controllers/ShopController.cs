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
            //var categories = _context.Categories.ToList();
            //return View(await _context.Categories.ToListAsync());

            //var categories = _context.Categories.ToList();
            //var products = null; // Fetch products

            //ViewBag.Categories = categories;
            //ViewBag.Products = null;

            //return View();
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
            //var selectedCategory = await _context.Categories
            //    .FirstOrDefaultAsync(c => c.Id == selectedCategoryId);
            //List<Article> productsInCategory;

            //if (selectedCategory == null)
            //{
            //    productsInCategory = null;
            //} else
            //{
            //    productsInCategory = await _context.Articles
            //        .Include(a => a.Category)
            //        .Where(a => a.CategoryId == selectedCategoryId)
            //        .ToListAsync();
            //}

            //ViewBag.Categories = await _context.Categories.ToListAsync();
            //ViewBag.Products = productsInCategory;

            //return View("Menu");
            var categories = await _context.Categories.ToListAsync();

            //var selectedCategory = await _context.Categories
            //    .Include(c => c.Articles)
            //    .FirstOrDefaultAsync(c => c.Id == model.SelectedCategoryId);

            var articles = await _context.Articles
                .Include(a => a.Category)
                .Where(a => a.CategoryId == model.SelectedCategoryId)
                .ToListAsync();

            model.Articles = articles;

            ViewData["Categories"] = new SelectList(categories, "Id", "Name", model.SelectedCategoryId);

            return View("Menu", model);
        }

            //// GET: Articles1
            //public async Task<IActionResult> Index()
            //{
            //    var shopDbContext = _context.Articles.Include(a => a.Category);
            //    return View(await shopDbContext.ToListAsync());
            //}

            //// GET: Articles1/Details/5
            //public async Task<IActionResult> Details(int? id)
            //{
            //    if (id == null)
            //    {
            //        return NotFound();
            //    }

            //    var article = await _context.Articles
            //        .Include(a => a.Category)
            //        .FirstOrDefaultAsync(m => m.Id == id);
            //    if (article == null)
            //    {
            //        return NotFound();
            //    }

            //    return View(article);
            //}

            //// GET: Articles1/Create
            //public IActionResult Create()
            //{
            //    ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            //    return View();
            //}

            //// POST: Articles1/Create
            //// To protect from overposting attacks, enable the specific properties you want to bind to.
            //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            //[HttpPost]
            //[ValidateAntiForgeryToken]
            //public async Task<IActionResult> Create([Bind("Id,EAN13,Name,Price,ImagePath,CategoryId")] Article article)
            //{
            //    if (ModelState.IsValid)
            //    {
            //        _context.Add(article);
            //        await _context.SaveChangesAsync();
            //        return RedirectToAction(nameof(Index));
            //    }
            //    ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", article.CategoryId);
            //    return View(article);
            //}

            //// GET: Articles1/Edit/5
            //public async Task<IActionResult> Edit(int? id)
            //{
            //    if (id == null)
            //    {
            //        return NotFound();
            //    }

            //    var article = await _context.Articles.FindAsync(id);
            //    if (article == null)
            //    {
            //        return NotFound();
            //    }
            //    ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", article.CategoryId);
            //    return View(article);
            //}

            //// POST: Articles1/Edit/5
            //// To protect from overposting attacks, enable the specific properties you want to bind to.
            //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            //[HttpPost]
            //[ValidateAntiForgeryToken]
            //public async Task<IActionResult> Edit(int id, [Bind("Id,EAN13,Name,Price,ImagePath,CategoryId")] Article article)
            //{
            //    if (id != article.Id)
            //    {
            //        return NotFound();
            //    }

            //    if (ModelState.IsValid)
            //    {
            //        try
            //        {
            //            _context.Update(article);
            //            await _context.SaveChangesAsync();
            //        }
            //        catch (DbUpdateConcurrencyException)
            //        {
            //            if (!ArticleExists(article.Id))
            //            {
            //                return NotFound();
            //            }
            //            else
            //            {
            //                throw;
            //            }
            //        }
            //        return RedirectToAction(nameof(Index));
            //    }
            //    ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", article.CategoryId);
            //    return View(article);
            //}

            //// GET: Articles1/Delete/5
            //public async Task<IActionResult> Delete(int? id)
            //{
            //    if (id == null)
            //    {
            //        return NotFound();
            //    }

            //    var article = await _context.Articles
            //        .Include(a => a.Category)
            //        .FirstOrDefaultAsync(m => m.Id == id);
            //    if (article == null)
            //    {
            //        return NotFound();
            //    }

            //    return View(article);
            //}

            //// POST: Articles1/Delete/5
            //[HttpPost, ActionName("Delete")]
            //[ValidateAntiForgeryToken]
            //public async Task<IActionResult> DeleteConfirmed(int id)
            //{
            //    var article = await _context.Articles.FindAsync(id);
            //    _context.Articles.Remove(article);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}

            //private bool ArticleExists(int id)
            //{
            //    return _context.Articles.Any(e => e.Id == id);
            //}
        }
}
