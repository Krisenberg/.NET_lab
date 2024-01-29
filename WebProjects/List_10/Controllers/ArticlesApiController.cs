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
    [Route("api/article")]
    [ApiController]
    public class ArticlesApiController : ControllerBase
    {
        private IRepository<Article> repository;
        public ArticlesApiController(ArticleRepository repo)
        {
            repository = repo;
        }

        // GET: api/article
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> Get()
        {
            return await repository.Get();
        }

        // GET api/article/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> Get(int id)
        {
           return await repository.Get(id);
        }

        // POST api/article
        [HttpPost]
        public async Task<ActionResult<Article>> Post([FromBody] Article art)
        {
            return await repository.Create(art);
        }

        // PUT api/article/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Article art)
        {
            return await repository.Update(id, art);

        }

        // DELETE api/article/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await repository.Delete(id);

            string cookieToBeDeleted = ((ArticleRepository)repository).CookieId;

            if (cookieToBeDeleted != null)
            {
                Response.Cookies.Delete(cookieToBeDeleted);
            }

            return res;
        }

        // GET: api/article/load
        [HttpGet("{categoryId}/{lastId}/{n}")]
        public async Task<ActionResult<List<Article>>> Get(int categoryId, int lastId, int n)
        {
            return await ((ArticleRepository)repository).GetNextNArticles(categoryId, lastId, n);
        }
    }
}
