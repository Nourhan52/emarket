using emarket.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace emarket.Controllers
{
    public class HomeController : Controller
    {
        private StoreEntities db = new StoreEntities();
        public ActionResult Index()
        {
            ViewBag.Cart = db.Carts.ToList();
            ViewBag.Product = db.Products.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Index(String searchCategory)
        {
            ViewBag.Cart = db.Carts.ToList();
            if (String.IsNullOrEmpty(searchCategory))
            {
                ViewBag.Product = db.Products.ToList();
            }
            else
            {

                var product = db.Products.Where(p => p.Category.name.Contains(searchCategory)).ToList();

                if (product.Count == 0 || product.Count == null)
                {
                    ViewBag.message = "No Result";
                }

                ViewBag.Product = product;

            }

            return View();
        }

        public ActionResult Addtocart(int id)
        {
            var search = db.Carts.Find(id);
            if (search == null)
            {
                Cart cart = new Cart();
                cart.product_id = id;
                cart.added_at = DateTime.Now;
                db.Carts.Add(cart);
                db.SaveChanges();
            }

           
            return RedirectToAction("Index");
        }
        public ActionResult DeleteProduct(int? id)
        {
            var product = db.Carts.SingleOrDefault(c => c.product_id ==id); ;

            if (product == null || id==null)
            {
                return RedirectToAction("Index", "Home");
            }
              
                    db.Carts.Remove(product);
                    db.SaveChanges();
            
            return RedirectToAction("DeleteProduct");
        }


        /*

        if (Session["Cart"]==null)
        {
            List<Cart> Cart = new List<Cart>();
            var product = db.Products.Find(id);
            Cart.Add(new Cart()
            {
                Product=product,
                added_at=DateTime.UtcNow,
            });
            Session["Cart"] = Cart;

        }
        else
        {
            List<Cart> Cart = (List<Cart>)Session["Cart"];
            var product = db.Products.Find(id);
            Cart.Add(new Cart()
            {
                Product = product,
                added_at = DateTime.Now,
            });
            Session["Cart"] = Cart;


        }
        return RedirectToAction("Index");

    }
  */
        //----------------------------
        public ActionResult View_Cart()
        {
            ViewBag.Cart = db.Carts.ToList();
            return View();
        }
        //<------------------------Add Product---------------------->//
        //HttpGet for Update:-----------------
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Cart = db.Carts.ToList();
            //to view cart list
            ViewBag.Category = db.Categories.ToList();
            //to list all categories
            return View();
        }
        //HttpPost for Update:-----------------
        [HttpPost]
        public object Add(Product product, HttpPostedFileBase upload)
        {   //create object product
            if (ModelState.IsValid)//check if sent model(object) = model in table -->input as required
            {
                //--------------change image format to a name--------------//
                String path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                //combine image with Uploads folder that exist in the server and save them in path variable 
                upload.SaveAs(path);
                //save image in this path
                product.image = upload.FileName;
                //save image name in product.image
                db.Products.Add(product);
                //add values to product table
                db.SaveChanges();
                //save the data in product table
                var add = db.Categories.SingleOrDefault(m => m.id == product.category_id);
                //to increace 1 in the choosen category
                add.number_of_products++;
                db.Entry(add).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                //save data 

            }
            return RedirectToAction("Index");
            //after add the product we return to the inxdex
        }
        //=========================================
       
public ActionResult Contact()
        {
            ViewBag.Cart = db.Carts.ToList();
            ViewBag.Message = "Your contact page";
            return View();
        }

    }
}