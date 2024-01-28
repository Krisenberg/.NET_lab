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

namespace List_10.Controllers
{
    [Route("api/article")]
    [ApiController]
    public class CategoriesApiController : ControllerBase
    {
        private IRepository<Article> repository;
        private IHostingEnvironment _hostingEnvironment;
        public CategoriesApiController(ShopDbContext context, IHostingEnvironment hostingEnvironment)
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
            return await repository.Update(art.Id, art);
        }

        // DELETE api/<ArticlesApiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await repository.Delete(id);
        }

    }
}
