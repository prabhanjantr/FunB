using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using FunB.Data;

namespace FunB.Controllers
{
    [EnableCors("*", "*", "*")]
    public class CategoriesController : ApiController
    {
        private FunBEntities1 db = new FunBEntities1();

        // GET: api/Categories
        public IQueryable<tbl_Categories> Gettbl_Categories()
        {
            return db.tbl_Categories;
        }

        // GET: api/Categories/5
        [ResponseType(typeof(tbl_Categories))]
        public async Task<IHttpActionResult> Gettbl_Categories(int id)
        {
            tbl_Categories tbl_Categories = await db.tbl_Categories.FindAsync(id);
            if (tbl_Categories == null)
            {
                return NotFound();
            }

            return Ok(tbl_Categories);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttbl_Categories(int id, tbl_Categories tbl_Categories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Categories.CategoryId)
            {
                return BadRequest();
            }

            db.Entry(tbl_Categories).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_CategoriesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Categories
        [ResponseType(typeof(tbl_Categories))]
        public async Task<IHttpActionResult> Posttbl_Categories(tbl_Categories tbl_Categories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Categories.Add(tbl_Categories);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Categories.CategoryId }, tbl_Categories);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(tbl_Categories))]
        public async Task<IHttpActionResult> Deletetbl_Categories(int id)
        {
            tbl_Categories tbl_Categories = await db.tbl_Categories.FindAsync(id);
            if (tbl_Categories == null)
            {
                return NotFound();
            }

            db.tbl_Categories.Remove(tbl_Categories);
            await db.SaveChangesAsync();

            return Ok(tbl_Categories);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_CategoriesExists(int id)
        {
            return db.tbl_Categories.Count(e => e.CategoryId == id) > 0;
        }
    }
}