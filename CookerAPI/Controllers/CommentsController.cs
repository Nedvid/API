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
using CookerAPI.DB;
using CookerAPI.Models;

namespace CookerAPI.Controllers
{
    public class CommentsController : ApiController
    {
        private CookerContext db = new CookerContext();

        // PUT: api/Comments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComment(int id, Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comment.Id_Comment)
            {
                return BadRequest();
            }

            db.Entry(comment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // POST: api/Comments
        [Authorize]
        [ResponseType(typeof(Comment_Details))]
        public IHttpActionResult PostComment(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Comments.Add(comment);
            db.SaveChanges();

            Comment_Details cd = new Comment_Details();
            cd.Name_User = db.Users.Where(x => x.Id_User == comment.Id_User).Select(x => x.Login).FirstOrDefault();
            cd.Text = comment.Text;
            cd.Date_Comment = comment.Date_Comment.ToShortDateString() + " " + comment.Date_Comment.ToShortTimeString();

            return Ok (cd);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentExists(int id)
        {
            return db.Comments.Count(e => e.Id_Comment == id) > 0;
        }
    }
}