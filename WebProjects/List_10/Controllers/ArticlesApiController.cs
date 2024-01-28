using List_10.Data;
using List_10.Models;
using List_10.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace List_10.Controllers
{
    //[Route("api/article")]
    //[ApiController]
    //public class ArticlesApiController : ControllerBase
    //{
    //    private readonly ShopDbContext _context;
    //    private IArticleRepository repository;
    //    private IHostingEnvironment _hostingEnvironment;
    //    public ArticlesApiController(IArticleRepository repo, ShopDbContext context, IHostingEnvironment hostingEnvironment) 
    //    {
    //        repository = repo;
    //        _context = context;
    //        _hostingEnvironment = hostingEnvironment;
    //        var articlesQuery = _context.Articles.Include(a => a.Category);
    //        var task = Task.Run(async () => { 
    //            var articlesList = await articlesQuery.ToListAsync();
    //            articlesList.ForEach(a => repository.AddArticle(a));
    //        });
    //        task.Wait();
    //    }

    //    // GET: api/<ArticlesApiController>
    //    [HttpGet]
    //    public IEnumerable<Article> Get() => repository.Articles;

    //    // GET api/<ArticlesApiController>/5
    //    [HttpGet("{id}")]
    //    public Article Get(int id) => repository[id];

    //    // POST api/<ArticlesApiController>
    //    [HttpPost]
    //    public async Task<Article> Post([FromBody] Article art)
    //    {
    //        _context.Add(art);
    //        await _context.SaveChangesAsync();
    //        return repository.AddArticle(art);
    //    }

    //    // PUT api/<ArticlesApiController>/5
    //    [HttpPut("{id}")]
    //    public async Task<Article> Put([FromBody] Article art)
    //    {
    //        try
    //        {
    //            _context.Update(art);
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!ArticleExists(art.Id))
    //            {
    //                return null;
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }
    //        return repository.UpdateArticle(art);
    //    }

    //    // DELETE api/<ArticlesApiController>/5
    //    [HttpDelete("{id}")]
    //    public async Task Delete(int id)
    //    {
    //        var article = await _context.Articles.FindAsync(id);

    //        if (article.ImagePath != null)
    //        {
    //            string absolutePath = Path.Combine(_hostingEnvironment.WebRootPath, article.ImagePath.TrimStart('~', '/'));
    //            if (System.IO.File.Exists(absolutePath))
    //            {
    //                System.IO.File.Delete(absolutePath);
    //            }
    //        }

    //        Response.Cookies.Delete("Article_" + article.Id.ToString());

    //        _context.Articles.Remove(article);
    //        await _context.SaveChangesAsync();
    //        repository.DeleteArticle(id);
    //    }

    //    private bool ArticleExists(int id)
    //    {
    //        return _context.Articles.Any(e => e.Id == id);
    //    }
    //}
    [Route("api/article")]
    [ApiController]
    public class ArticlesApiController : ControllerBase
    {
        private IRepository<Article> repository;
        private IHostingEnvironment _hostingEnvironment;
        public ArticlesApiController(ShopDbContext context, IHostingEnvironment hostingEnvironment)
        {
            repository = new ArticleRepository(context);
        }

        // GET: api/<ArticlesApiController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> Get()
        {
            return await repository.Get();
        }

        // GET api/<ArticlesApiController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> Get(int id)
        {
           return await repository.Get(id);
        }

        // POST api/<ArticlesApiController>
        [HttpPost]
        public async Task<ActionResult<Article>> Post([FromBody] Article art)
        {
            return await repository.Create(art);
        }

        // PUT api/<ArticlesApiController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Article art)
        {
            return await repository.Update(art);
        }

        // DELETE api/<ArticlesApiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await repository.Delete(id);
        }

    }
}
