using List_10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace List_10.Repositories
{
    public abstract class RepositoryBase<T> : ControllerBase, IRepository<T> where T : class //constraint to specify that T is a reference type
    {
        protected readonly DbContext _context;
        protected DbSet<T> _dbSet;
        //protected readonly Dictionary<int, T> articles;

        public RepositoryBase(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<ActionResult<T>> Create(T entity)
        {
           _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<IActionResult> Delete(int id)
        {
            var data = await _dbSet.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            _dbSet.Remove(data);
            await _context.SaveChangesAsync();
            return Ok();
        }

        public async Task<ActionResult<IEnumerable<T>>> Get()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<ActionResult<T>> Get(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return entity;
        }

        public async Task<IActionResult> Update(int id, T entity)
        {
            //var existingEntity = await _dbSet.FindAsync(id);
            //if (existingEntity == null)
            //{
            //    return NotFound();
            //}
            ////_context.Attach(entity);
            ////_context.Entry(entity).State = EntityState.Modified;

            //try
            //{
            //    _context.Update(entity);
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    throw;
            //}

            //return Ok();
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty != null)
            {
                var idValue = (int) idProperty.GetValue(entity);
                if (id != idValue)
                {
                    return BadRequest();
                }
            }

            var existingOrder = await _dbSet.FindAsync(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            _context.Entry(existingOrder).CurrentValues.SetValues(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }
    }
}
