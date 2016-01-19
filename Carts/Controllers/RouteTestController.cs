using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Carts.Controllers
{
    public class RouteTestController : Controller
    {
        // GET: RouteTest
        public ActionResult Index()
        {
            var result = Models.RouteTest.TempProducts.getAllproducts();
            return View(result);
        }

        public ActionResult Index2(string id)
        {
            return Content(String.Format("id的值為:{0}", id));
        }

        public ActionResult Index3(string id, string page)
        {
            return Content(String.Format("id的值為:{0}, page的值為:{1}", id, page));
        }
    }
}