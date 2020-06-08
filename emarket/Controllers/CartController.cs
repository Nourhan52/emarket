using emarket.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace emarket.Controllers
{
    public class CartController : Controller
    {

        private StoreEntities db = new StoreEntities();
        public ActionResult Index()
        {
            ViewBag.Cart = db.Carts.ToList();
            return View(db.Products.OrderByDescending(x => x.id).ToList());
           
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
            var product = db.Carts.SingleOrDefault(c => c.product_id == id); ;

            if (product == null || id == null)
            {
                return RedirectToAction("Index", "Cart");
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
    }
}
        /*StoreEntities db = new StoreEntities();
        public ActionResult Index()
        {

            return View(db.Products.OrderByDescending(x => x.id).ToList());
        }


    }*/










/*
            * 
            * 
            *    public ActionResult Addtocart(int id)
           {
           var search = db.Carts.Find(id);
               if(search == null)
               {
                   Cart cart = new Cart();
                   cart.product_id = id;
                   cart.added_at = DateTime.Now;
                   db.Carts.Add(cart);
                   db.SaveChanges();
               }

                return RedirectToAction("Index");
           }

            * 
            * Product p = db.Products.Where(x => x.id == Id).SingleOrDefault();

               return View(p);

       }
       List<Cart> li = new List<Cart>();
       [HttpPost]
       public ActionResult Addtocart(Product product, int Id)
       {
           Product p = db.Products.Where(x => x.id == Id).SingleOrDefault();
           Cart c = new Cart();
           c.product_id = p.id;


           li.Add(c);
           TempData["cart"] = li;
           TempData.Keep();
           return RedirectToAction("Index");
       }
       public ActionResult Checkout()
       {
           return View();
       }
   }*/

//---------------

