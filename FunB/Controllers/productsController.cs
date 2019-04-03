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
    [RoutePrefix("api/products")]
    public class productsController : ApiController
    {
        private FunBEntities1 db = new FunBEntities1();

        // GET: api/products
        public IQueryable<product> Getproducts()
        {
            return db.products;
        }

        // GET: api/products/5
        [ResponseType(typeof(product))]
        public async Task<IHttpActionResult> Getproduct(int id)
        {
            product product = await db.products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/products/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putproduct(int id, product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productExists(id))
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

        // POST: api/products
        [ResponseType(typeof(product))]
        public async Task<IHttpActionResult> Postproduct(product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.products.Add(product);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        // DELETE: api/products/5
        [ResponseType(typeof(product))]
        public async Task<IHttpActionResult> Deleteproduct(int id)
        {
            product product = await db.products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            db.products.Remove(product);
            await db.SaveChangesAsync();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool productExists(int id)
        {
            return db.products.Count(e => e.Id == id) > 0;
        }
    }
}