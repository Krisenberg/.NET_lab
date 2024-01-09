using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using List_11.Data;
using List_11.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace List_11.Pages.Articles
{
    public class CreateModel : PageModel
    {
        private readonly List_11.Data.ShopDbContext _context;
        private IHostingEnvironment _hostingEnvironment;

        public CreateModel(List_11.Data.ShopDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Article Article { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (Article.ImageFile != null && Article.ImageFile.Length > 0)
                {
                    string uniqueFileName = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + "_" + Path.GetFileName(Article.ImageFile.FileName);
                    string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "upload");
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    Article.ImagePath = "~/upload/" + uniqueFileName;

                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Article.ImageFile.CopyToAsync(fileStream);
                    }
                }
                _context.Articles.Add(Article);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
