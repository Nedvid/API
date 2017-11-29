using CookerAPI.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CookerAPI.Models;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CookerAPI.Controllers
{
    public class Other_Controller : ApiController
    {
        private CookerContext db = new CookerContext();


        // GET: api/users/login=email/
        [Authorize]
        [ResponseType(typeof(UserDetail))]
        [Route("api/Users/login={login}")]
        public async Task<IHttpActionResult> GetUser(string login)
        {
            var user = await db.Users.SingleOrDefaultAsync(x => x.Login== login);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        
        //GET: api/black_items/id=id_user
        [Authorize]
        [ResponseType(typeof(Product))]
        [Route("api/Black_items/id={id_user}")]
        public async Task<IHttpActionResult> GetBlackItems(int id_user)
        {
            List<Black_Item> items = db.Black_Items.Where(x => x.Id_User == id_user).ToList();

            List<Black_Item_Detail> products = new List<Black_Item_Detail>();

            foreach (var item in items)
            {
                Black_Item_Detail bid = new Black_Item_Detail();
                bid.Id_Black_Item = item.Id_Black_Item;
                bid.Id_Product = item.Id_Product;
                bid.Id_User = item.Id_User;
                bid.Product_Name = db.Products.Where(x => x.Id_Product == item.Id_Product).Select(x => x.Name_Product).FirstOrDefault();
                products.Add(bid);
            }

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        //DELETE: api/black_items/id=id_user/id_p=id_product
        [Authorize]
        [HttpDelete]
        [Route("api/Black_items/id={id_user}/id_p={id_product}")]
        public IHttpActionResult DeleteBlackItem(int id_user, int id_product)
        {
            Black_Item bi = db.Black_Items.SingleOrDefault(x => x.Id_User == id_user && x.Id_Product == id_product);
            db.Black_Items.Remove(bi);
            db.SaveChanges();
            return Ok("Removed");
        }

        //POST: api/black_items/id=id_user/name=product
        [Authorize]
        [HttpPost]
        [Route("api/Black_items/id={id_user}/name={product}")]
        public IHttpActionResult DeleteBlackItem(int id_user, string product)
        {
            Product p = db.Products.SingleOrDefault(x => x.Name_Product == product);

            if(p==null)
            {
                p = new Product();
                p.Name_Product = product;
                p.Visible = true;
                db.Products.Add(p);
                db.SaveChanges();

                Black_Item bi = new Black_Item();
                bi.Id_Product = p.Id_Product;
                bi.Id_User = id_user;
                db.Black_Items.Add(bi);
                db.SaveChanges();
            }
            else
            {
                Black_Item bi = new Black_Item();
                bi.Id_Product = p.Id_Product;
                bi.Id_User = id_user;
                db.Black_Items.Add(bi);
                db.SaveChanges();
            }

            return Ok("Added");
        }

        //GET: api/items/id=id_list
        [Authorize]
        [ResponseType(typeof(string))]
        [Route("api/items/id={id_list}")]
        public async Task<IHttpActionResult> GetItems(int id_list)
        {

            string s_items = db.Lists.Where(x => x.Id_List == id_list).Select(x => x.Items).FirstOrDefault();
            string[] items = s_items.Split('\t');

            if (items == null)
            {
                return NotFound();
            }

            return Ok(items);
        }
    }
}
