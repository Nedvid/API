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
    public class Category_MainController : ApiController
    {
        private CookerContext db = new CookerContext();

        // GET: api/Category_Main
        public IQueryable<Category_Main> GetCategories_Main()
        {
            return db.Categories_Main;
        }

        // GET: api/Category_Main/5
        [ResponseType(typeof(Category_Main))]
        public IHttpActionResult GetCategory_Main(int id)
        {
            Category_Main category_Main = db.Categories_Main.Find(id);
            if (category_Main == null)
            {
                return NotFound();
            }

            return Ok(category_Main);
        }

        // PUT: api/Category_Main/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategory_Main(int id, Category_Main category_Main)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category_Main.Id_Category_Main)
            {
                return BadRequest();
            }

            db.Entry(category_Main).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Category_MainExists(id))
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

        // POST: api/Category_Main
        [ResponseType(typeof(Category_Main))]
        public IHttpActionResult PostCategory_Main(Category_Main category_Main)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories_Main.Add(category_Main);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category_Main.Id_Category_Main }, category_Main);
        }

        // DELETE: api/Category_Main/5
        [ResponseType(typeof(Category_Main))]
        public IHttpActionResult DeleteCategory_Main(int id)
        {
            Category_Main category_Main = db.Categories_Main.Find(id);
            if (category_Main == null)
            {
                return NotFound();
            }

            db.Categories_Main.Remove(category_Main);
            db.SaveChanges();

            return Ok(category_Main);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Category_MainExists(int id)
        {
            return db.Categories_Main.Count(e => e.Id_Category_Main == id) > 0;
        }
    }
}