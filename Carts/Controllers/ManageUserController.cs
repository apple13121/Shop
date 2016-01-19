using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Carts.Controllers
{
    public class ManageUserController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ResultMessage = TempData["ResultMessage"];
            using (Models.UserEntities db = new Models.UserEntities())
            {//抓所有AspNetUsers中的資料，並放入model.ManagerUser中
                var result = (from s in db.AspNetUsers
                              select new Models.ManageUser
                              {
                                  Id = s.Id,
                                  UserName = s.UserName,
                                  Email = s.Email
                              }).ToList();
                return View(result);
            }
        }

        //編輯會員資訊
        public ActionResult Edit(string id)
        {
            using (Models.UserEntities db = new Models.UserEntities())
            {
                var result = (from s in db.AspNetUsers
                              where s.Id == id
                              select new Models.ManageUser
                              {
                                  Id = s.Id,
                                  UserName = s.UserName,
                                  Email = s.Email
                              }).FirstOrDefault();
                if( result != default(Models.ManageUser))
                {
                    return View(result);
                }
            }
            //Error Message
            TempData["ResultMessage"] = String.Format("使用者[{0}]不存在，請重新操作", id);
            return RedirectToAction("Index");
        }

        //將修改的會員資料，存入資料庫
        [HttpPost]
        public ActionResult Edit(Models.ManageUser postback)
        {
            using (Models.UserEntities db = new Models.UserEntities())
            {
                var result = (from s in db.AspNetUsers
                              where s.Id == postback.Id
                              select s).FirstOrDefault();
                if (result != default(Models.AspNetUsers))
                {
                    result.UserName = postback.UserName;
                    result.Email = postback.Email;
                    db.SaveChanges();
                    //設定成功訊息
                    TempData["ResultMessage"] = String.Format("使用者[{0}]成功編輯", postback.UserName);
                    return RedirectToAction("Index");
                }
            }
            //設定錯誤訊息
            TempData["ResultMessage"] = String.Format("使用者[{0}]不存在，請重新操作", postback.UserName);
            return RedirectToAction("Index");
        }
    }
}