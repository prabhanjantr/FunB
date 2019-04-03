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
    public class CartsController : ApiController
    {
        private FunBEntities1 db = new FunBEntities1();

        // GET: api/Carts
        public IQueryable<tblShoppingcart> GettblShoppingcarts()
        {
            return db.tblShoppingcarts;
        }

        // GET: api/Carts/5
        [ResponseType(typeof(tblShoppingcart))]
        public async Task<IHttpActionResult> GettblShoppingcart(int id)
        {
            tblShoppingcart tblShoppingcart = await db.tblShoppingcarts.FindAsync(id);
            if (tblShoppingcart == null)
            {
                return NotFound();
            }

            return Ok(tblShoppingcart);
        }

        // PUT: api/Carts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblShoppingcart(int id, tblShoppingcart tblShoppingcart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblShoppingcart.ID)
            {
                return BadRequest();
            }

            db.Entry(tblShoppingcart).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblShoppingcartExists(id))
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

        // POST: api/Carts
        [ResponseType(typeof(tblShoppingcart))]
        public async Task<IHttpActionResult> PosttblShoppingcart(tblShoppingcart tblShoppingcart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblShoppingcarts.Add(tblShoppingcart);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblShoppingcart.ID }, tblShoppingcart);
        }

        // DELETE: api/Carts/5
        [ResponseType(typeof(tblShoppingcart))]
        public async Task<IHttpActionResult> DeletetblShoppingcart(int id)
        {
            tblShoppingcart tblShoppingcart = await db.tblShoppingcarts.FindAsync(id);
            if (tblShoppingcart == null)
            {
                return NotFound();
            }

            db.tblShoppingcarts.Remove(tblShoppingcart);
            await db.SaveChangesAsync();

            return Ok(tblShoppingcart);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblShoppingcartExists(int id)
        {
            return db.tblShoppingcarts.Count(e => e.ID == id) > 0;
        }
    }
}