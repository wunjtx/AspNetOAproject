using SimpleOA.Model.OAModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using SimpleOA.Common;
using FineUIMvc;

namespace SimpleOA.UI.Portal.Controllers
{
    public class HomeController : SimpleOA.UI.Portal.Controllers.BaseController
    {
        IBLL.ITreeMenuBll treeMenuBll = new BLL.TreeMenuBll();

        public HomeController()
        {
            this.LoginCheck = true;
        }

        public ActionResult Index()
        {
            //CheckSession();
            //GetMenu();
            LoadData();
            return View();
        }

        private IEnumerable<TreeMenu> GetMenu()
        {
            return treeMenuBll.GetEntities(u => u.MenuId>=0).ToList();
        }

        private void CheckSession()
        {
            throw new NotImplementedException();
        }

        private void LoadData()
        {
            List<TreeNode> nodes = new List<TreeNode>();
            //get all menus
            //List<TreeMenu> treeMenus = GetMenu().Where(u=>u.MenuId>=0).ToList();
            //-------------

            //get user menus
            int uId = this.LoginUser.ID;
            var lgUser = userInfoBll.GetEntities(u => u.ID == uId).FirstOrDefault();
            var roles = lgUser.RoleInfo;
            var actions = roles.SelectMany<RoleInfo, TreeMenu>(r => r.TreeMenu) ;
            List<TreeMenu> treeMenus = actions.Where<TreeMenu>(t=>t.MenuId >= 0).Distinct<TreeMenu>().ToList();

            for (int i = 0; i < treeMenus.Count; i++)
            {
                if (treeMenus[i].pId==0)
                {
                    TreeNode tn = new TreeNode();
                    tn.Text = treeMenus[i].MenuName;
                    tn.NodeID = treeMenus[i].Id.ToString();
                    nodes.Add(tn);

                    List<TreeMenu> nodeMenus = treeMenus.Where(u => u.pId == treeMenus[i].Id).ToList();
                    if (nodeMenus!=null&&nodeMenus.Count>0)
                    {
                        ResolveSubTree(nodeMenus,tn);
                    }
                }
            }
            // 视图数据
            ViewBag.treeMenuNodes = nodes.ToArray();
        }

        private void ResolveSubTree(List<TreeMenu> nodeMenus, TreeNode treeNode)
        {
            foreach (TreeMenu tr in nodeMenus)
            {
                TreeNode node = new TreeNode();
                node.Text = tr.MenuName;
                node.NavigateUrl = tr.Url;
                node.NodeID = tr.Id.ToString();
                treeNode.Nodes.Add(node);
                List<TreeMenu> tm = nodeMenus.Where(u => u.pId == tr.Id).ToList();
                if (nodeMenus != null && nodeMenus.Count > 0)
                {
                    ResolveSubTree(tm, node);
                }
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}