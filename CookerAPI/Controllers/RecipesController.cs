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
using CookerAPI.Models;
using CookerAPI.DB;
using System.Threading.Tasks;

namespace CookerAPI.Controllers
{
    public class Param
    {
        public int category_main { get; set; }
        public int [] categories { get; set; }
        public int [] products { get;set;}
        public int [] black_products { get; set; }
    }

    public class RecipesController : ApiController
    {
        private CookerContext db = new CookerContext();

        // GET: api/Recipes
        public IQueryable<Recipe> GetRecipes()
        {
            return db.Recipes;
        }

        // GET: api/Recipes/
        [ResponseType(typeof(List<Recipe>))]
        [Route("api/RecipesParam")]
        public List<Recipe> GetRecipesParam([FromUri] Param param)
        {
            List<Recipe> recipes = db.Recipes.ToList();

            if(param.category_main != 0)
            {
                recipes = recipes.Where(x => x.Id_Category_Main == param.category_main).ToList();
            }

            if (param.categories!=null)
            {
                List<int> recipes_id = new List<int>();
                foreach(var item in param.categories)
                {
                    recipes_id.AddRange(db.Categories_Recipes.Where(x => x.Id_Category == item).Select(x=> x.Id_Recipe).ToList());
                }

                recipes = recipes.Where(x => recipes_id.Contains(x.Id_Recipe)).ToList();
            }

            if (param.products != null)
            {
                IEnumerable<int> recipes_id = new List<int>();
                bool first = true;
                foreach (var item in param.products)
                {
                    if (first)
                    {
                        recipes_id = db.Elements.Where(x => x.Id_Product == item).Select(x => x.Id_Recipe).ToList();
                        first = false;
                    }
                    else
                    {
                        recipes_id = recipes_id.Intersect(db.Elements.Where(x => x.Id_Product == item).Select(x => x.Id_Recipe).ToList());
                    }
                }

                recipes = recipes.Where(x => recipes_id.Contains(x.Id_Recipe)).ToList();

            }

            if (param.black_products != null)
            {
                List<int> recipes_id = new List<int>();
                foreach (var item in param.black_products)
                {
                    recipes_id.AddRange(db.Elements.Where(x => x.Id_Product == item).Select(x => x.Id_Recipe).ToList());
                }

                recipes = recipes.Where(x => !recipes_id.Contains(x.Id_Recipe)).ToList();
            }

            return recipes;
        }

        // GET: api/recipes/id=id_recipe
        [ResponseType(typeof(Recipe_Details))]
        [Route("api/Recipes/id={id_recipe}")]
        public async Task<IHttpActionResult> GetRecipe(int id_recipe)
        {
            var recipe = await db.Recipes.SingleOrDefaultAsync(x => x.Id_Recipe == id_recipe);
            var r_d = new Recipe_Details();
            r_d.Id_Recipe = recipe.Id_Recipe;
            r_d.Name_Recipe = recipe.Name_Recipe;
            r_d.Rate = recipe.Rate;
            r_d.Level = recipe.Level;
            r_d.Date_Recipe = recipe.Date_Recipe;
            r_d.URL_Photo = recipe.URL_Photo;
            r_d.Time = recipe.Time;
            r_d.Number_Person = recipe.Number_Person;
            r_d.Steps = recipe.Steps;
            r_d.Instruction = recipe.Instruction;

            r_d.Name_User = db.Users.Where(x => x.Id_User == recipe.Id_User).Select(x => x.Login).FirstOrDefault();
            r_d.Category_Main = db.Categories_Main.Where(x => x.Id_Category_Main == recipe.Id_Category_Main).Select(x => x.Name_Category_Main).FirstOrDefault();

            #region Categories
            List<Category_Recipe> categories = db.Categories_Recipes.Where(x => x.Id_Recipe == recipe.Id_Recipe).ToList();
            List<string> ct = new List<string>();
            foreach (var item in categories)
            {
                ct.Add(db.Categories.Where(x => x.Id_Category_Recipe == item.Id_Category).Select(x => x.Name_Category_Recipe).FirstOrDefault());
            }
            r_d.Categories = ct;
            #endregion
            #region Elements
            List<Element> elements = db.Elements.Where(x => x.Id_Recipe == recipe.Id_Recipe).ToList();
            List<Element_Details> e_d = new List<Element_Details>();

            foreach (var item in elements)
            {
                e_d.Add(new Element_Details()
                {
                    Name_Product = db.Products.Where(x => x.Id_Product == item.Id_Product).Select(x => x.Name_Product).FirstOrDefault(),
                    Quantity = item.Quantity
                });
            }

            r_d.Elements_Details = e_d;

            #endregion
            #region Comments
            List<Comment> comments = db.Comments.Where(x => x.Id_Recipe == recipe.Id_Recipe).ToList();
            List<Comment_Details> cd = new List<Comment_Details>();
            foreach (var item in comments)
            {
                cd.Add(new Comment_Details()
                {
                    Name_User = db.Users.Where(x => x.Id_User == item.Id_User).Select(x => x.Login).FirstOrDefault(),
                    Text = item.Text,
                    Date_Comment = item.Date_Comment.ToShortDateString() + " " + item.Date_Comment.ToShortTimeString()
                });
            }
            r_d.Comments_Details = cd;
            #endregion

            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(r_d);
        }

        // PUT: api/Recipes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRecipe(int id, Recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != recipe.Id_Recipe)
            {
                return BadRequest();
            }

            db.Entry(recipe).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
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

        // POST: api/Recipes
        [Authorize]
        [ResponseType(typeof(Recipe))]
        public IHttpActionResult PostRecipe(Recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            recipe.Visible = false;

            db.Recipes.Add(recipe);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = recipe.Id_Recipe }, recipe);
        }

        // DELETE: api/Recipes/5
        [ResponseType(typeof(Recipe))]
        public IHttpActionResult DeleteRecipe(int id)
        {
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return NotFound();
            }

            db.Recipes.Remove(recipe);
            db.SaveChanges();

            return Ok(recipe);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RecipeExists(int id)
        {
            return db.Recipes.Count(e => e.Id_Recipe == id) > 0;
        }
    }
}