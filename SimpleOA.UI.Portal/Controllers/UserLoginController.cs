using FineUIMvc;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleOA.BLL;
using SimpleOA.IBLL;
using SimpleOA.Model.OAModel;
using System.Text.RegularExpressions;
using SimpleOA.Common;

namespace SimpleOA.UI.Portal.Controllers
{
    //[LoginCheckFilter(LoginCheck = false)]
    public class UserLoginController : SimpleOA.UI.Portal.Controllers.BaseController
    {
        private Common.MemcacheSession ms = new MemcacheSession();
        private Common.ValidateCode vc = new Common.ValidateCode();
        private Regex rx = new Regex(@"[0-9]\d*");

        public UserLoginController()
        {
            this.LoginCheck = false;
        }

        // GET: Basic/LoginCaptcha
        public ActionResult Index()
        {
            InitData();

            return View();
        }

        private void InitData()
        {
            rs.Delete("Captcha");
            string imageHTML = InitCaptcha();
            ViewBag.ImgCaptchaText = imageHTML;
        }

        private string InitCaptcha()
        {
            string code = vc.CreateValidateCode(4);
            //session
            //Session["Captcha"] = code;
            //-------
            //memcache session
            //ms.SetValue<string>(code, "Captcha");
            //----------------
            //redis session
            rs.SetItem<string>("Captcha", code,SessionId);

            string imageUrl = Url.Content("~/UserLogin/CreateCaptchaImg?t=" + DateTime.Now.Ticks);

            return String.Format("<img id=\"captchaImg\" src=\"{0}\" />", imageUrl);
        }

        public ActionResult CreateCaptchaImg()
        {
            //session
            //byte[] buffer = vc.CreateValidateGraphic(Session["Captcha"] as string);
            //-------
            //memcache session
            //string codeStr = ms.GetValue<string>("Captcha");
            //----------------
            //redis session
            
            string codeStr = rs.GetItem<string>("Captcha");

            MatchCollection mc = rx.Matches(codeStr);
            string code = mc.Count > 0 ? mc[0].ToString() : "";
            byte[] buffer = vc.CreateValidateGraphic(code);
            return File(buffer, "image/jpeg");
        }

        /// <summary>
        /// 4 digit
        /// </summary>
        /// <returns></returns>
        private string GenerateRandomCode()
        {
            string s = String.Empty;
            Random random = new Random();
            for (int i = 0; i < 4; i++)
            {
                s += random.Next(10).ToString();
            }
            return s;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult imgCaptcha_Click()
        {
            string imageHTML = InitCaptcha();

            UIHelper.LinkButton("imgCaptcha").Text(imageHTML, encodeText: false);

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnLogin_Click(FormCollection values)
        {
            string strCode = values["txtCaptcha"].Trim();
            //-----------
            //string sessionCode = Session["Captcha"] as string;
            //once compared session, set it to null
            //Session["Captcha"] = null;
            //-----------
            //memcache session
            //string code = ms.GetValue<string>("Captcha");
            //----------------
            //redis session
            string code = rs.GetItem<string>("Captcha");
            MatchCollection mc = rx.Matches(code);
            string sessionCode = mc.Count > 0 ? mc[0].ToString() : string.Empty;
            //memcache session
            //ms.Delete("Captcha");
            //----------------

            //redis session
            rs.Delete("Captcha");

            //memcache method
            //Common.CheckUserIpByMemcache checkip = new Common.CheckUserIpByMemcache();
            //---------------
            //redis method
            Common.CheckUserIpByRedis checkip = new Common.CheckUserIpByRedis();
            //if (checkip.CheckIpAttemptTimes(Enum.AttemptTimesEnum.Thirty.ToInt(),Enum.ExpMinEnum.HalfHour.ToInt()))
            if (checkip.CheckIpAttemptTimes(100, Enum.ExpMinEnum.HalfHour.ToInt()))
            {
                if (string.IsNullOrEmpty(strCode) || string.IsNullOrEmpty(sessionCode) || strCode != sessionCode)
                {
                    ShowNotify("Captcha Error！",MessageBoxIcon.Error);
                    var ln = UIHelper.LinkButton("imgCaptcha");
                    ln.Text(InitCaptcha(), encodeText: false);
                    return UIHelper.Result();
                }
                else
                {
                    string remember = values["rememberMe"].Trim();
                    string name = values["txtName"].Trim();
                    string pwd =Common.Encrypt.Md5String( values["txtPwd"].Trim());
                    short delNormal = (short)SimpleOA.Enum.DelFlagEnum.Normal;
                    UserInfo loginUser = userInfoBll.GetEntities(u => (u.UName == name && u.Pwd == pwd && u.DelFlag == delNormal)).FirstOrDefault();
                    if (loginUser != null)
                    {
                        //-----------------
                        //Session["loginUser"] = loginUser;
                        //-----------------
                        //memcache session
                        //ms.SetValue<UserInfo>(loginUser,"loginUser",DateTime.Now.AddMinutes(Enum.ExpMinEnum.HalfHour.ToInt())) ;
                        //----------------
                        //redis session, 
                        rs.SetItem<UserInfo>( "loginUser", loginUser, SessionId);

                        if (remember.ToLower()=="true")
                        {
                            HttpCookie h1 = new HttpCookie("UserName",loginUser.UName);
                            h1.Expires = DateTime.Now.AddMinutes(Enum.ExpMinEnum.TwoHour.ToInt());
                            Request.Cookies.Add(h1);
                            HttpCookie h2 = new HttpCookie("UserPwd", Common.Encrypt.Md5String(loginUser.Pwd));
                            h2.Expires = h1.Expires;
                            Request.Cookies.Add(h2);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ShowNotify("User Name or Password Error！", MessageBoxIcon.Error);
                        var ln = UIHelper.LinkButton("imgCaptcha");
                        ln.Text(InitCaptcha(), encodeText: false);
                        var txtName = UIHelper.TextBox("txtName");
                        txtName.Text(string.Empty);
                        var txtPwd = UIHelper.TextBox("txtPwd");
                        txtPwd.Text(string.Empty);
                        var txtCaptcha = UIHelper.TextBox("txtCaptcha");
                        txtCaptcha.Text(string.Empty);
                        return UIHelper.Result();
                    }
                }
            }
            else
            {
                ShowNotify("Attempt Over 30 Times！", MessageBoxIcon.Error);
                return UIHelper.Result();
            }
        }
    }
}