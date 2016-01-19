using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Carts.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            //宣告<Product>result，等等要接db撈出來的資料
            List<Models.Cart.Product> result = new List<Models.Cart.Product>();

            //接收訊息
            ViewBag.ResultMessage = TempData["ResultMessage"];

            //DB把資料取出
            using (Models.Cart.CartDBContext db = new Models.Cart.CartDBContext())
            {
                result = (from s in db.Products select s).ToList();

                return View(result);
            }
        }

        //建立商品頁面
        public ActionResult Create()
        {
            return View();
        }

        //建立商品頁面 - 資料傳回資料庫
        [HttpPost]
        public ActionResult Create(Models.Cart.Product postback)
        {
            if (this.ModelState.IsValid)//資料驗證成功
            {
                using (Models.Cart.CartDBContext db = new Models.Cart.CartDBContext())
                {
                    //回傳資料postback加入Products
                    db.Products.Add(postback);

                    //存取異動資料
                    db.SaveChanges();

                    //設定成功訊息
                    TempData["ResultMessage"] = String.Format("商品[{0}]成功建立", postback.Name);

                    return RedirectToAction("Index");
                }
            }
            //失敗訊息
            ViewBag.ResultMessage = "資料有誤請檢查";

            return View(postback);
        }

        //編輯商品頁面(從資料庫中取出此資料資訊)
        public ActionResult Edit(int id)
        {
            using (Models.Cart.CartDBContext db = new Models.Cart.CartDBContext())
            {
                //抓取Product.Id 輸入id的資料
                var result = (from s in db.Products where s.Id == id select s).FirstOrDefault();
                if(result != default(Models.Cart.Product))//判斷此id是否有資料
                {
                    return View(result);
                }
                else
                {
                    TempData["ResultMessage"] = "資料有誤，請重新操作";
                    return RedirectToAction("Index");
                }
            }
        }

        //編輯商品頁面(將修改過後的資料，存入資料庫中)
        [HttpPost]
        public ActionResult Edit(Models.Cart.Product postback)
        {
            if(this.ModelState.IsValid) //判斷使用者輸入資料是否正確
            {
                using (Models.Cart.CartDBContext db = new Models.Cart.CartDBContext())
                {
                    var result = (from s in db.Products where s.Id == postback.Id select s).FirstOrDefault();

                    //儲存使用者變更資料
                    result.Name = postback.Name; result.Price = postback.Price;
                    result.PublishDate = postback.PublishDate; result.Quantity = postback.Quantity;
                    result.Status = postback.Status; result.CategoryId = postback.CategoryId;
                    result.DefaultImageId = postback.DefaultImageId; result.Description = postback.Description;
                    result.DefaultImageURL = postback.DefaultImageURL;

                    //存入資料庫
                    db.SaveChanges();

                    //成功後回index頁面
                    TempData["ResultMessage"] = String.Format("商品[{0}]成功編輯", postback.Name);
                    return RedirectToAction("Index");
                }
            }
            else //如果資料不正確則回到自己的頁面(Edit)
            {
                return View(postback);
            }
        }

        //刪除商品
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (Models.Cart.CartDBContext db = new Models.Cart.CartDBContext())
            {
                //抓取product id
                var result = (from s in db.Products where s.Id == id select s).FirstOrDefault();
                if(result != default(Models.Cart.Product))//判斷id是否有資料
                {
                    db.Products.Remove(result);

                    db.SaveChanges();

                    //設定成功訊息並導回index頁面
                    TempData["ResultMessage"] = String.Format("商品[{0}]成功刪除", result.Name);
                    return RedirectToAction("Index");
                }
                else
                {  //如果沒有資料則顯示錯誤，並回index頁面
                    TempData["ResultMessage"] = "指定資料不存在，無法刪除，請重新操作";
                    return RedirectToAction("Index");
                }
            }
        }

    }
}