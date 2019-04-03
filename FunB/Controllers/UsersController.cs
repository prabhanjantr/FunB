using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using FunB.Data;
using Microsoft.IdentityModel.Tokens;

namespace FunB.Controllers
{
    [EnableCors(origins: "http://localhost:4200",headers: "*",methods: "*")]
    [RoutePrefix("api/User")]
    public class UsersController : ApiController
    {
        private FunBEntities1 db = new FunBEntities1();
        private object cookieIdentity;

        [Route("Login")]
        [HttpPost]
        public async Task<IHttpActionResult> Login(User user)
        {
            var userfound = db.Users.Where(u => u.Email == user.Email).FirstOrDefault();
            if (userfound != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
            
            
        }

        // GET: api/Users
    public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!FindUser(user))
                {
                    user.createdDate = DateTime.Now;
                    db.Users.Add(user);
                    await db.SaveChangesAsync();
                }
                else
                {
                    return BadRequest("User already registered");
                }
            }
            catch (Exception e)
            {
                db.Dispose();
            }

            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
        private bool FindUser(User user)
        {
            var userfound = db.Users.Where(u => u.Email == user.Email).FirstOrDefault();
            return userfound == null ? false : true;


        }
    }
}