using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Chirper.API.Infrastructure;
using Chirper.API.Models;

namespace Chirper.API.Controllers
{
    public class CommentsController : ApiController
    {
        private ChirperDataContext db = new ChirperDataContext();



        // POST: api/Comments
        [ResponseType(typeof(Comment))]
        public IHttpActionResult PostComment(Comment comment)
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

            comment.UserId = user.Id;

            DateTime thisDate2 = DateTime.Now;
            comment.CreatedDate = thisDate2.ToString("MMMM dd, yyyy");
            comment.CreatedTime = thisDate2.ToString("h:mm tt");

            db.Comments.Add(comment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = comment.CommentID }, comment);
        }

        // DELETE: api/Comments/5
        [ResponseType(typeof(Comment))]
        public IHttpActionResult DeleteComment(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }

            db.Comments.Remove(comment);
            db.SaveChanges();

            return Ok(comment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostExists(int id)
        {
            return db.Posts.Count(e => e.PostId == id) > 0;
        }
    }
}