using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FineUIMvc;
using SimpleOA.BLL;
using SimpleOA.Model.OAModel;

namespace SimpleOA.UI.Portal.Controllers
{
    public class UserController : BaseController
    {
        UserInfoBll userInfoBll = new UserInfoBll();
        UserQueryParam qm = new UserQueryParam( );
        RoleInfoBll roleInfoBll = new RoleInfoBll();
        private Regex rx = new Regex(@"[0-9]\d*");

        public UserController()
        {
            this.LoginCheck = true;
            qm.pageCount = 3;
        }
        
        public ActionResult Add()
        {
            InitData();
            return View();
        }

        private void InitData()
        {
            var roles = roleInfoBll.GetEntities(r => true).ToList();
            ViewBag.CheckBoxList2DataSource = roles;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult filePhoto_FileSelected  (HttpPostedFileBase FileUpload1, FormCollection values)
        {
            if (FileUpload1 != null)
            {
                string fileName = FileUpload1.FileName;

                if (!ValidateFileType(fileName))
                {
                    // 清空文件上传组件
                    UIHelper.FileUpload("filePhoto").Reset();

                    ShowNotify("无效的文件类型！");
                }
                else
                {
                    fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                    fileName = DateTime.Now.Ticks.ToString() + "_" + fileName;

                    FileUpload1.SaveAs(Server.MapPath("/Upload/" + fileName));

                    UIHelper.Image("imgPhoto").ImageUrl("/Upload/" + fileName);
                    UIHelper.FileUpload("FileUpload1").Disabled(true);
                    // 清空文件上传组件（上传后要记着清空，否则点击提交表单时会再次上传！！）
                    //UIHelper.FileUpload("filePhoto").Reset();
                }
            }
             return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnSubmit_Click(FormCollection values,string RoleList)
        {
            var filePhoto = UIHelper.FileUpload("filePhoto");

            var imgPhotoUrl = values["imgPhotoUrl"];

            if (!string.IsNullOrEmpty( imgPhotoUrl) && imgPhotoUrl.EndsWith("/res/images/blank.png"))
            {
                filePhoto.MarkInvalid("请先上传个人头像！");
                ShowNotify("请先上传个人头像！");
            }
            else
            {
                //UIHelper.Label("labResult").Text("用户名：" + values["tbxUserName"] + "<br/>" +
                //        "邮箱：" + values["tbxEmail"] + "<br/>" +
                //        "<p>头像：<br /><img src=\"" + imgPhotoUrl + "\" /></p>",
                //        encodeText: false);

                // 清空表单字段
                //UIHelper.Image("imgPhoto").ImageUrl(Url.Content("~/res/images/blank.png"));
                UIHelper.FileUpload("FileUpload1").Disabled(true);
                //filePhoto.Reset();
                UIHelper.TextBox("UName").Disabled(true);
                UIHelper.TextBox("Pwd").Disabled(true);
                UIHelper.TextBox("ShowName").Disabled(true);
                UIHelper.TextBox("Remark").Disabled(true);
                UserInfo userInfo = new UserInfo();
                userInfo.DelFlag = 0;
                userInfo.Pwd = Common.Encrypt.Md5String(values["Pwd"]);
                userInfo.UName = values["UName"];
                userInfo.ShowName = values["ShowName"];
                userInfo.SubTime = DateTime.Now;
                userInfo.ModfiedOn = DateTime.Now;
                userInfo.Remark = values["Remark"];
                MatchCollection mc = rx.Matches(values["RoleList"]);
                if (mc!=null && mc.Count>0)
                {
                    RoleInfo role;
                    foreach (var m in mc)
                    {
                        int intm = Convert.ToInt32(m.ToString());
                        role = roleInfoBll.GetEntities(r => r.ID == intm).FirstOrDefault();
                        userInfo.RoleInfo.Add(role);
                    }
                }
                userInfoBll.Add(userInfo);
            }
            return UIHelper.Result();
        }
        // GET: User
        public ActionResult Index()
        {
            LoadData();
            return View();
        }
        public ActionResult SearchIndex(string uqmStr)
        {
            LoadSearchData(uqmStr);
            return View();
        }

        private void LoadData()
        {
            var users = userInfoBll.GetEntities(u=>true);
            if (users!=null && users.Count<UserInfo>()>0)
            {
                int  recordCount ;
                
                // .获取当前分页数据
                //ViewBag.Grid1DataSource = userInfoBll.GetEntitiesIndex(0, 3, out recordCount, u => true, s => s.ID, true).ToList();
                //ViewBag.Grid1DataSource = userInfoBll.GetSearchAll(qm, out recordCount).OrderBy(u=>u.ID).Skip(qm.pageCount*qm.pageIndex).Take(qm.pageCount).ToList();
                //ViewBag.Grid1DataSource = userInfoBll.GetSearchIndex(qm, u => u.ID, out recordCount).OrderBy(u => u.ID).Skip(qm.pageCount * qm.pageIndex).Take(qm.pageCount).ToList();
                ViewBag.Grid1DataSource = userInfoBll.GetSearchIndex(qm, u => u.ID, out recordCount).ToList();
                ViewBag.qm = Common.SerializerHelper.SerializerToString(qm);
                // .设置总项数（特别注意：数据库分页初始化时，一定要设置总记录数RecordCount）
                ViewBag.Grid1RecordCount = recordCount;
            }
        }

        private void LoadSearchData(string uqmStr)
        {
            //FormCollection fm = Common.SerializerHelper.DeserializeToObject<FormCollection>(fmStr);
            //qm = Common.SerializerHelper.DeserializeToObject<UserQueryParam>(fm["FormQm"]);
            int recordCount;
            UserQueryParam uqm = Common.SerializerHelper.DeserializeToObject<UserQueryParam>(uqmStr);
            var users = userInfoBll.GetSearchIndex(uqm, u => u.ID, out recordCount).ToList();
            //grid1.DataSource(users,new string[] { "ID", "UName", "ShowName", "Remark", "SubTime" } , true);
            //grid1.RecordCount(recordCount);
            ViewBag.Grid1DataSource = users;
            ViewBag.Grid1RecordCount = recordCount;
            ViewBag.qm = Common.SerializerHelper.SerializerToString(uqm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Button1_Click(FormCollection qfm)
        {
            //qm = Common.SerializerHelper.DeserializeToObject<UserQueryParam>(qfm["FormQm"]);
            if (!string.IsNullOrEmpty(qfm["ID"]))
            {
                qm.ID = Convert.ToInt32(qfm["ID"]);
            }
            if (!string.IsNullOrEmpty(qfm["UName"]))
            {
                qm.UName = qfm["UName"];
            }
            if (!string.IsNullOrEmpty(qfm["ShowName"]))
            {
                qm.ShowName = qfm["ShowName"];
            }
            if (!string.IsNullOrEmpty(qfm["Remark"]))
            {
                qm.Remark = qfm["Remark"];
            }
            if (!string.IsNullOrEmpty(qfm["TimeAfter"]))
            {
                qm.TimeAfter = Convert.ToDateTime(qfm["TimeAfter"]);
            }
            if (!string.IsNullOrEmpty(qfm["TimeBfore"]))
            {
                qm.TimeBefore = Convert.ToDateTime(qfm["TimeAfter"]);
            }
            //var grid1 = UIHelper.Grid("Grid1");
            string qmStr = Common.SerializerHelper.SerializerToString(qm);
            return RedirectToAction("SearchIndex",new { uqmStr = qmStr });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Grid1_PageIndexChanged(FormCollection fm, string[] Grid1_fields,string SearchQm, int Grid1_pageIndex)
        {
            var grid1 = UIHelper.Grid("Grid1");
            UserQueryParam uqm = Common.SerializerHelper.DeserializeToObject<UserQueryParam>(SearchQm);
            if (uqm.ID>0)
            {
                qm.ID = uqm.ID;
            }
            if (!string.IsNullOrEmpty(uqm.UName))
            {
                qm.UName = uqm.UName;
            }
            if (!string.IsNullOrEmpty(uqm.ShowName))
            {
                qm.ShowName = uqm.ShowName;
            }
            if (!string.IsNullOrEmpty(uqm.Remark))
            {
                qm.Remark = uqm.Remark;
            }
            if (uqm.TimeAfter>DateTime.MinValue)
            {
                qm.TimeAfter = uqm.TimeAfter;
            }
            if (uqm.TimeBefore > DateTime.MinValue)
            {
                qm.TimeBefore = uqm.TimeBefore;
            }
            int recordCount ;

            qm.pageIndex = Grid1_pageIndex;
            //qm.orderBy = u => u.ID;
            // .获取当前分页数据
            //var dataSource = userInfoBll.GetEntitiesIndex(pageIndex: Grid1_pageIndex, pageCount: 3,Total :out recordCount,whereLambda:u=>true,orderbyLambda:s=>s.ID,isAsc:true).ToList();
            //var dataSource = userInfoBll.GetSearchAll(qm, out recordCount).OrderBy(u => u.ID).Skip(qm.pageCount * qm.pageIndex).Take(qm.pageCount).ToList();
            var dataSource = userInfoBll.GetSearchIndex(qm, u => u.ID, out recordCount).ToList();
            // .设置总项数（数据库分页回发时，如果总记录数不变，可以不设置RecordCount）
            grid1.RecordCount(recordCount);
            
            grid1.DataSource(dataSource, new string[] { "ID", "UName", "ShowName", "Remark", "SubTime" }, false);
            ViewBag.qm =Common.SerializerHelper.SerializerToString( qm);
            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnSaveHideRefresh_Click(FormCollection fm)
        {
            int ID;
            string n = fm["UName"];
            // 1. 这里放置保存窗体中数据的逻辑
            if (!string.IsNullOrEmpty(fm["UID"]) && int.TryParse(fm["UID"],out ID))
            {
                UserInfo user = userInfoBll.GetEntities(u => u.ID == ID).FirstOrDefault();
                if (user!=null)
                {
                    user.UName = fm["UName"];
                    user.Pwd = fm["Pwd"];
                    user.Remark = fm["Remark"];
                    user.ShowName = fm["ShowName"];
                    user.SubTime = Convert.ToDateTime(fm["SubTime"]);
                    
                    //MatchCollection mc = rx.Matches(fm["RoleList[]"]);
                    //if (mc != null && mc.Count > 0)
                    //{
                    //    RoleInfo role;
                    //    foreach (var m in mc)
                    //    {
                    //        int intm = Convert.ToInt32(m.ToString());
                    //        role = roleInfoBll.GetEntities(r => r.ID == intm).FirstOrDefault();
                    //        user.RoleInfo.Add(role);
                    //    }
                    //}
                    MatchCollection mc = rx.Matches(fm["RoleList"]);
                    if (mc != null && mc.Count > 0)
                    {
                        user.RoleInfo.Clear();
                        RoleInfo role;
                        foreach (var m in mc)
                        {
                            int intm = Convert.ToInt32(m.ToString());
                            role = roleInfoBll.GetEntities(r => r.ID == intm).FirstOrDefault();
                            user.RoleInfo.Add(role);
                        }
                    }
                }
                userInfoBll.Update(user);
            }
            
            // 2. 隐藏本窗体，然后刷新父窗体
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            
            return UIHelper.Result();
        }

        public ActionResult Detail(int ID, string UName)
        {
            var roles = roleInfoBll.GetEntities(r => true);
            var user = userInfoBll.GetEntities(u => u.ID == ID).FirstOrDefault();
            if (user!=null)
            {
                ViewBag.RoleListDataSource = roles;
                ViewBag.RoleListSelectedValueArray = user.RoleInfo.Select<RoleInfo,string>(r=>r.ID.ToString()).ToArray();
                ViewBag.ID = ID.ToString();
                ViewBag.User = user;
            }
            return View();
        }
    }
}