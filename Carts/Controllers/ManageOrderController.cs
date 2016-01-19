using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Carts.Controllers
{
    public class ManageOrderController : Controller
    {
        public ActionResult Index()
        {//取得Order中所有資料
            using (Models.Cart.CartDBContext db = new Models.Cart.CartDBContext())
            {
                var result = (from s in db.Orders
                              select s).ToList();
                return View(result);
            }
        }

        public ActionResult Details(int id)
        {
            using (Models.Cart.CartDBContext db = new Models.Cart.CartDBContext())
            {
                //取得OrderId為傳入id的所有商品列表
                var result = (from s in db.OrderDetails
                              where s.OrderId == id
                              select s).ToList();
                if (result.Count == 0)
                {//如果商品數目為零，代表該訂當異常
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