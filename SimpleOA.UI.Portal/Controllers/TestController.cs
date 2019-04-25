using SimpleOA.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleOA.Model.OAModel;

namespace SimpleOA.UI.Portal.Controllers
{
    public class TestController : Controller
    {
        UserInfoBll userInfoBll = new UserInfoBll();

        public ActionResult Test()
        {
            int i1 = 0;
            int i2 = 10;

            int i = i2 / i1;
            return View();
        }

        // GET: Test
        public ActionResult Index()
        {
            ViewData.Model= userInfoBll.GetEntities(u => true);
            return View();
        }

        // GET: Test/Details/5
        public ActionResult Details(int id)
        {
            ViewData.Model = userInfoBll.GetEntities(u => u.ID == id).FirstOrDefault();
            return View();
        }

        // GET: Test/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test/Create
        [HttpPost]
        public ActionResult Create(UserInfo userInfo)
        {
            try
            {
                // TODO: Add insert logic here
                userInfoBll.Add(userInfo);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Test/Edit/5
        public ActionResult Edit(int id)
        {
            UserInfo user = userInfoBll.GetEntities(u => u.ID == id).FirstOrDefault();
            ViewData.Model = user;
            return View();
        }

        // POST: Test/Edit/5
        [HttpPost]
        public ActionResult Edit(UserInfo user)
        //public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                userInfoBll.Update(user);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Test/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Test/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
