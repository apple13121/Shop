using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Carts.Controllers
{
    public class TestController : Controller
    {
        public ActionResult GetCart()
        {
            //取得目前購物車
            var cart = Models.Cart.Operation.GetCurrentCart();
            cart.AddProduct(1);
            
            return Content(string.Format("目前購物車總共:{0}元", cart.TotalAmout));
        }
    }
}