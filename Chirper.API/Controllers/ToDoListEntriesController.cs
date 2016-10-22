using Chirper.API.Infrastructure;
using Chirper.API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Chirper.API.Controllers
{
    public class ToDoListEntriesController : ApiController
    {
        private ChirperDataContext db = new ChirperDataContext();

        // GET: api/ToDoListEntries
        [Authorize]
        public IQueryable<ToDoListEntry> GetToDoListEntries()
        {
            return db.ToDoListEntries;
        }

        // GET: api/ToDoListEntries/5
        [ResponseType(typeof(ToDoListEntry))]
        [Authorize]
        public IHttpActionResult GetToDoListEntry(int id)
        {
            ToDoListEntry todolistentry = db.ToDoListEntries.Find(id);
            if (todolistentry == null)
            {
                return NotFound();
            }

            return Ok(todolistentry);
        }

        // PUT: api/ToDoListEntries/5
        [ResponseType(typeof(void))]
        [Authorize]
        public IHttpActionResult PutToDoListEntry(int id, ToDoListEntry todolistentry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != todolistentry.ToDoListEntryId)
            {
                return BadRequest();
            }

            db.Entry(todolistentry).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoListEntryExists(id))
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

        // POST: api/ToDoListEntries
        [Authorize]
        [ResponseType(typeof(ToDoListEntry))]
        public IHttpActionResult ToDoListEntry(ToDoListEntry todolistentry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get the username printed on the incoming token
            string username = User.Identity.Name;

            // Get the actual user from the database (may return null if not found!)
            var user = db.Users.FirstOrDefault(u => u.UserName == username);

            if (user == null) { return Unauthorized(); }

            todolistentry.UserId = user.Id;


            DateTime thisDate1 = DateTime.Now;
            todolistentry.CreatedDate = thisDate1.ToString("MMMM dd, yyyy");
            todolistentry.CreatedTime = thisDate1.ToString("h:mm tt");

            db.ToDoListEntries.Add(todolistentry);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = todolistentry.ToDoListEntryId }, todolistentry);
        }

        // DELETE: api/ToDoListEntries/5
        [ResponseType(typeof(ToDoListEntry))]
        [Authorize]
        public IHttpActionResult DeleteToDoListEntry(int id)
        {
            ToDoListEntry todolistentry = db.ToDoListEntries.Find(id);
            if (todolistentry == null)
            {
                return NotFound();
            }

            db.ToDoListEntries.Remove(todolistentry);
            db.SaveChanges();

            return Ok(todolistentry);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ToDoListEntryExists(int id)
        {
            return db.ToDoListEntries.Count(e => e.ToDoListEntryId == id) > 0;
        }
    }
}
