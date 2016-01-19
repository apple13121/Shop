using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Carts.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using(Models.Cart.CartDBContext db = new Models.Cart.CartDBContext())
            {
                var result = (from s in db.Products select s).ToList();
                return View(result);
            }
        }

        public ActionResult Fruit()
        {
            using (Models.Cart.CartDBContext db = new Models.Cart.CartDBContext())
            {
                var result = (from s in db.Products where s.CategoryId == 1 select s).ToList();
                return View(result);
            }
        }

        public ActionResult Vegetable()
        {
            using (Models.Cart.CartDBContext db = new Models.Cart.CartDBContext())
            {
                var result = (from s in db.Products where s.CategoryId == 2 select s).ToList();
                return View(result);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Detail(int id)
        {
            using (Models.Cart.CartDBContext db = new Models.Cart.CartDBContext())
            {
                var result = (from s in db.Products
                              where s.Id == id
                              select s).FirstOrDefault();

                if (result == default(Models.Cart.Product))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(result);
                }
            }
        }
    }
}