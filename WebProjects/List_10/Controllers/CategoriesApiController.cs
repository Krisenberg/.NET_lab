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
    [Route("api/category")]
    [ApiController]
    public class CategoriesApiController : ControllerBase
    {
        private IRepository<Category> repository;
        public CategoriesApiController(CategoryRepository repo)
        {
            repository = repo;
        }

        // GET: api/category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            return await repository.Get();
        }

        // GET api/category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            return await repository.Get(id);
        }

        // POST api/category
        [HttpPost]
        public async Task<ActionResult<Category>> Post([FromBody] Category cat)
        {
            return await repository.Create(cat);
        }

        // PUT api/category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Category cat)
        {
            return await repository.Update(id, cat);

        }

        // DELETE api/category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await repository.Delete(id);

            List<string> cookiesToBeDeleted = ((CategoryRepository)repository).CookieIds.ToList();
            cookiesToBeDeleted.ForEach(cookie => Response.Cookies.Delete(cookie));

            return res;
        }

    }
}
