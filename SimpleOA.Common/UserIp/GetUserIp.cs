using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SimpleOA.Common
{
    public class GetUserIp
    {
        /// <summary>
        /// get ip
        /// </summary>
        /// <returns>return Ip</returns>
        public string GetLoginIp()
        {
            string loginip = "";
            //Request.ServerVariables[""]--get server variable 
            if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null) //if remote ip is null
            {
                //get remote Ip
                loginip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            //if proxy
            else if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    //get real Ip
                    loginip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else
                {
                    //get user IP 
                    loginip = HttpContext.Current.Request.UserHostAddress;
                }
            }
            else
            {
                //get user IP
                loginip = HttpContext.Current.Request.UserHostAddress;
            }
            return loginip;
        }
    }
}
