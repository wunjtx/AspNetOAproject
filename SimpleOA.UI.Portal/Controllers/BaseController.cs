using FineUIMvc;
using SimpleOA.Model.OAModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using SimpleOA.Common;

namespace SimpleOA.UI.Portal.Controllers
{
    public class BaseController : Controller
    {
        public IBLL.IUserInfoBll userInfoBll = new BLL.UserInfoBll();
        public bool LoginCheck = true;
        public UserInfo LoginUser { get; set; }
        public Common.RedisSession rs = new RedisSession();
        public string SessionId;
        public IBLL.IActionInfoBll actionInfoBll = new BLL.ActionInfoBll();
        public IBLL.IRoleInfoBll roleInfoBll = new BLL.RoleInfoBll();
        //public Common.MemcacheSession ms = new MemcacheSession();
        #region static readonly


        #endregion

        #region OnActionExecuting

        /// <summary>
        /// before action excuting
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (LoginCheck)
            {
                //session
                //if (filterContext.HttpContext.Session["loginUser"] == null)
                //-------
                //memcache session
                //if(ms.GetValue<UserInfo>("loginUser") ==null)
                //----------------
                //redis session
                if (rs.GetItem<UserInfo>("loginUser") == null)
                {
                    string userName = filterContext.HttpContext.Request.Cookies["UserName"]==null?null: filterContext.HttpContext.Request.Cookies["UserName"].ToString();
                    string userPwd = filterContext.HttpContext.Request.Cookies["UserPwd"]==null?null: filterContext.HttpContext.Request.Cookies["UserPwd"].ToString();
                    if (userName  != null && userPwd != null)
                    {
                        LoginUser = userInfoBll.GetEntities(u => u.UName == userName && u.Pwd == userPwd).FirstOrDefault();
                        if (LoginUser != null)
                        {
                            //memcache session
                            //ms.SetValue<UserInfo>(LoginUser, "loginUser", DateTime.Now.AddMinutes(Enum.ExpMinEnum.HalfHour.ToInt()));
                            //----------------
                            //redis session
                            string url = filterContext.HttpContext.Request.Url.AbsolutePath.ToLower();
                            string httpMethod = filterContext.HttpContext.Request.HttpMethod.ToUpper();
                            var menus = LoginUser.RoleInfo.SelectMany<RoleInfo, TreeMenu>(r => r.TreeMenu).Distinct().ToList();
                            var urls = menus.Where(m => m.Url.Replace(" ", "").ToLower() == url.TrimEnd('/')).FirstOrDefault();
                            if (urls!=null&& url.Contains(url))
                            {
                                SessionId = Guid.NewGuid().ToString();
                                rs.CreateSessionId(SessionId);
                                rs.SetItem<UserInfo>("loginUser", LoginUser, SessionId);
                            }
                            else
                            {
                                filterContext.Result = new RedirectResult("/UserLogin/Index");
                                return;
                            }
                        }
                        else
                        {
                            //filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                            //return;
                            filterContext.Result = new RedirectResult(filterContext.HttpContext.Request.MapPath("/UserLogin/Index"));
                            return;
                        }
                    }
                    else
                    {
                        filterContext.Result = new RedirectResult(filterContext.HttpContext.Request.MapPath("/UserLogin/Index"));
                        return;
                    }
                }
                else
                {
                    //session
                    //LoginUser = filterContext.HttpContext.Session["loginUser"] as UserInfo;
                    //-------
                    //memcache session
                    //LoginUser = ms.GetValue<UserInfo>("loginUser");
                    //ms.SetValue<UserInfo>(LoginUser, "loginUser", DateTime.Now.AddMinutes(30));
                    //----------------"/Workflow/Detail""/workflow/detail/"
                    //redis session

                    LoginUser = rs.GetItem<UserInfo>("loginUser");
                    int uId = LoginUser.ID;
                    UserInfo user = userInfoBll.GetEntities(u => u.ID == uId).FirstOrDefault();
                    string url = filterContext.HttpContext.Request.Url.AbsolutePath.ToLower();
                    string httpMethod = filterContext.HttpContext.Request.HttpMethod.ToUpper();
                    var menus = user.RoleInfo.SelectMany<RoleInfo, TreeMenu>(r => r.TreeMenu).Distinct().ToList();
                    var urls = menus.Where(m => m.Url.Replace(" ","").ToLower() == url.TrimEnd('/')).FirstOrDefault();
                    if (urls != null)
                    {
                        SessionId = HttpContext.Request.Cookies["RedisSessionId"].Value;
                    }
                    else
                    {
                        filterContext.Result = new RedirectResult("/UserLogin/Index");
                        return;
                    }
                    //rs.SetItem<UserInfo>("loginUser", LoginUser, SessionId);
                }
            }
            else
            {
                if (HttpContext.Request.Cookies["RedisSessionId"]!=null)
                {
                    SessionId = HttpContext.Request.Cookies["RedisSessionId"].Value;
                }
                else {
                SessionId = Guid.NewGuid().ToString();
                rs.CreateSessionId(SessionId);
                }
            }
        }


        #endregion

        #region check upload file ext

        protected readonly static List<string> VALID_FILE_TYPES = new List<string> { "jpg", "bmp", "gif", "jpeg", "png" };

        protected static bool ValidateFileType(string fileName)
        {
            string fileType = String.Empty;
            int lastDotIndex = fileName.LastIndexOf(".");
            if (lastDotIndex >= 0)
            {
                fileType = fileName.Substring(lastDotIndex + 1).ToLower();
            }

            if (VALID_FILE_TYPES.Contains(fileType))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion

        #region grid

        /// <summary>
        /// selected row
        /// </summary>
        /// <param name="grid">grid</param>
        /// <returns>row desc</returns>
        protected string HowManyRowsAreSelected(Grid grid)
        {
            StringBuilder sb = new StringBuilder();
            int selectedCount = grid.SelectedRowIndexArray.Length;
            if (selectedCount > 0)
            {
                sb.AppendFormat("<p><strong>selected {0} row：</strong></p>", selectedCount);
                sb.Append("<table class=\"result\">");

                sb.Append("<tr><th>Row NO.</th>");
                //foreach (string datakey in grid.DataKeyNames)
                //{
                //    sb.AppendFormat("<th>{0}</th>", datakey);
                //}
                sb.Append("</tr>");


                for (int i = 0; i < selectedCount; i++)
                {
                    int rowIndex = grid.SelectedRowIndexArray[i];
                    sb.Append("<tr>");

                    int rownumber = rowIndex + 1;
                    if (grid.AllowPaging)
                    {
                        rownumber += grid.PageIndex * grid.PageSize;
                    }
                    sb.AppendFormat("<td>{0}</td>", rownumber);


                    // if memory pagination, all page data is loaded，rowIndex is the index of the all the page data not current page index
                    if (grid.AllowPaging && !grid.IsDatabasePaging)
                    {
                        rowIndex = grid.PageIndex * grid.PageSize + rowIndex;
                    }

                    //object[] dataKeys = grid.DataKeys[rowIndex];
                    //for (int j = 0; j < dataKeys.Length; j++)
                    //{
                    //    sb.AppendFormat("<td>{0}</td>", dataKeys[j]);
                    //}

                    sb.Append("</tr>");
                }
                sb.Append("</table>");
            }
            else
            {
                sb.Append("<strong>No Row SelectedS！</strong>");
            }

            return sb.ToString();
        }

        /// <summary>
        /// get gender ，for ASPX use
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        protected string GetGender(object gender)
        {
            if (Convert.ToInt32(gender) == 1)
            {
                return "male";
            }
            else
            {
                return "female";
            }
        }

        #endregion

        #region 

        /// <summary>
        /// show notify dialog
        /// </summary>
        /// <param name="fields"></param>
        public virtual void ShowNotify(FormCollection values)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Form Field：");
            sb.Append("<ul class=\"result\">");
            foreach (string key in values.Keys)
            {
                if (key == "__RequestVerificationToken")
                {
                    continue;
                }
                sb.AppendFormat("<li>{0}: {1}</li>", key, values[key]);
            }
            sb.Append("</ul>");

            ShowNotify(sb.ToString());
        }

        /// <summary>
        /// show notify dialog
        /// </summary>
        /// <param name="message"></param>
        public virtual void ShowNotify(string message)
        {
            ShowNotify(message, MessageBoxIcon.Information);
        }

        /// <summary>
        /// show notify dialog
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageIcon"></param>
        public virtual void ShowNotify(string message, MessageBoxIcon messageIcon)
        {
            ShowNotify(message, messageIcon, Target.Top);
        }

        /// <summary>
        /// show notify dialog
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageIcon"></param>
        /// <param name="target"></param>
        public virtual void ShowNotify(string message, MessageBoxIcon messageIcon, Target target)
        {
            Notify n = new Notify();
            n.Target = target;
            n.Message = message;
            n.MessageBoxIcon = messageIcon;
            n.PositionX = Position.Center;
            n.PositionY = Position.Top;
            n.DisplayMilliseconds = 3000;
            n.ShowHeader = false;

            n.Show();
        }

        /// <summary>
        /// full url path
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public string GetAbsoluteUrl(string virtualPath)
        {
            // http://benjii.me/2015/05/get-the-absolute-uri-from-asp-net-mvc-content-or-action/
            var urlBuilder = new System.UriBuilder(Request.Url.AbsoluteUri)
            {
                Path = Url.Content(virtualPath),
                Query = null,
            };

            return urlBuilder.ToString();
        }


        /// <summary>
        /// load xml file
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <returns></returns>
        protected XmlDocument LoadXml(string xmlPath)
        {
            // load xml file
            xmlPath = Server.MapPath(xmlPath);
            string xmlContent = String.Empty;
            using (StreamReader sr = new StreamReader(xmlPath))
            {
                xmlContent = sr.ReadToEnd();
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlContent);

            return doc;
        }

        #endregion


    }
    
}