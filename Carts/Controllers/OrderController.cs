using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Carts.Controllers
{
    public class OrderController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        /*public ActionResult Index(Models.OrderModel.Ship postback)
        {
            return View();
        }*/
        public ActionResult Index(Models.OrderModel.Ship postback)
        {
            if(this.ModelState.IsValid)
            {
                //取得目前的購物車
                var currentcart = Models.Cart.Operation.GetCurrentCart();

                //取得目前登入使用Id
                var userId = HttpContext.User.Identity.GetUserId();

                using (Models.Cart.CartDBContext db = new Models.Cart.CartDBContext())
                {
                    //建立Order物件
                    var order = new Models.Cart.Order()
                    {
                        UserId = userId,
                        RecieverName = postback.RecieverName,
                        RecieverPhone = postback.RecieverPhone,
                        RecieverAddress = postback.RecieverAddress
                    };
                    //加入Orders資料表後，儲存變更
                    db.Orders.Add(order);
                    db.SaveChanges();

                    //取得購物車中OrderDetail物件
                    var orderDetails = currentcart.ToOrderDetailList(order.Id);

                    //將其加入OrderDetails資料表後，儲存變更
                    db.OrderDetails.AddRange(orderDetails);
                    db.SaveChanges();
                }
                return RedirectToAction("OrderSuccess");
            }
            return View();
        }
        
        public ActionResult MyOrder()
        {
            var userId = HttpContext.User.Identity.GetUserId();

            using (Models.Cart.CartDBContext db = new Models.Cart.CartDBContext())
            {
                var result = (from s in db.Orders
                              where s.UserId == userId
                              select s).ToList();

                return View(result);
            }
        }

        public ActionResult MyOrderDetail(int id)
        {
            using (Models.Cart.CartDBContext db = new Models.Cart.CartDBContext())
            {
                var result = (from s in db.OrderDetails
                              where s.OrderId == id
                              select s).ToList();

                if(result.Count ==0 )
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(result);
                }
            }
        }

        public ActionResult OrderSuccess()
        {
            ViewBag.Message = "謝謝您的惠顧，歡迎再度光臨!";

            return View();
        }


    }
}