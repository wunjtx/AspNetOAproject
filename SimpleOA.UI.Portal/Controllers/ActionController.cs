using FineUIMvc;
using SimpleOA.BLL;
using SimpleOA.Model.OAModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SimpleOA.UI.Portal.Controllers
{
    public class ActionController : BaseController
    {
        public ActionController()
        {
            this.LoginCheck = true;
            mq.pageCount = 5;
        }
        RoleInfoBll roleInfoBll = new RoleInfoBll();
        TreeMenuBll treeMenuBll = new TreeMenuBll();
        MenuQueryParam mq = new MenuQueryParam();
        private Regex rx = new Regex(@"[0-9]\d*");
        // GET: Action
        public ActionResult Index()
        {
            LoadData();
            return View();
        }

        private void LoadData()
        {
            int recordCount;
            IQueryable<TreeMenu> tms = treeMenuBll.GetEntitiesIndex<int>(0, mq.pageCount,out recordCount, u => true,u=>u.Id,true);
            ViewBag.Grid1DataSource = tms.ToList();
            ViewBag.Grid1RecordCount = recordCount;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Grid1_PageIndexChanged(string[] Grid1_fields, int Grid1_pageIndex)
        {
            var grid1 = UIHelper.Grid("Grid1");
            int recordCount;
            List<TreeMenu> tms = treeMenuBll.GetEntitiesIndex<int>( Grid1_pageIndex, mq.pageCount, out recordCount,u=>true,u=>u.Id,true).ToList();
            grid1.RecordCount(recordCount);
            grid1.DataSource(tms, Grid1_fields, true);
            return UIHelper.Result();
        }

        public ActionResult Detail(int Id)
        {
            TreeMenu tm = treeMenuBll.GetEntities(u => u.Id == Id).FirstOrDefault();
            ViewBag.Id = tm.Id.ToString();
            ViewBag.tm = tm;
            var roles= roleInfoBll.GetEntities(r => true).ToList();
            ViewBag.RoleListDataSource = roles;
            ViewBag.RoleListSelectedValueArray = tm.RoleInfo.Select<RoleInfo,string>(r=>r.ID.ToString()).ToArray();
            return View(tm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnSaveHideRefresh_Click(FormCollection fm)
        {
            if (!string.IsNullOrEmpty(fm["Id"]))
            {
                int id = Convert.ToInt32(fm["Id"]);
                var treeMenu = treeMenuBll.GetEntities(t => t.Id == id).FirstOrDefault();
                if (treeMenu != null)
                {
                    MatchCollection mc = rx.Matches(fm["RoleList"]);
                    if (mc != null && mc.Count > 0)
                    {
                        treeMenu.RoleInfo.Clear();
                        RoleInfo role;
                        foreach (var m in mc)
                        {
                            int intm = Convert.ToInt32(m.ToString());
                            role = roleInfoBll.GetEntities(r => r.ID == intm).FirstOrDefault();
                            treeMenu.RoleInfo.Add(role);
                        }
                    }
                }
                treeMenuBll.Update(treeMenu);
                ViewBag.Id = id.ToString();
                ViewBag.tm = treeMenu;
                ViewBag.RoleListDataSource= roleInfoBll.GetEntities(r => true).ToList();
                ViewBag.RoleListSelectedValueArray = treeMenu.RoleInfo.Select<RoleInfo,String>(r=>r.ID.ToString()).ToArray();
            }
            return UIHelper.Result();
        }
    }
}