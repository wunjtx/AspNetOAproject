using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SimpleOA.Common
{
    public class MemcacheSession
    {
        HttpCookie cookie = HttpContext.Current.Request.Cookies["MemcacheSessionId"];
        public T GetValue<T>(string key)
        {
            string cookieValue = cookie == null?null: cookie.Value;
            if (cookieValue != null)
            {
                if (MemcacheHelper.Get(cookieValue+"$"+key) != null)
                {
                    return SerializerHelper.DeserializeToObject<T>( MemcacheHelper.Get(cookieValue + "$" + key).ToString()) ;
                }
                else
                {
                    return default(T);
                }
            }
            else
            {
                return default(T);
            }
        }
        public void SetValue<T>(T value,string key)
        {
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                MemcacheHelper.Set(HttpContext.Current.Response.Cookies["MemcacheSessionId"].Value + "$" + key, SerializerHelper.SerializerToString(value));
            }
            else
            {
                HttpCookie cookie1 = new HttpCookie("MemcacheSessionId");
                string SessionId = cookie1.Value = Guid.NewGuid().ToString();
                HttpContext.Current.Response.Cookies.Add(cookie1);
                MemcacheHelper.Set(SessionId + "$" + key, SerializerHelper.SerializerToString(value));
            }
        }
        public void SetValue<T>(T value, string key, DateTime expireMinutes)
        {
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                MemcacheHelper.Set(cookie.Value + "$" + key, SerializerHelper.SerializerToString(value));
            }
            else { 
                string cookieValue = cookie.Value = Guid.NewGuid().ToString();
                MemcacheHelper.Set(cookieValue + "$" + key, SerializerHelper.SerializerToString(value), expireMinutes);
            }
        }
        public bool DeleteSession(string key)
        {
            if (HttpContext.Current.Request.Cookies.AllKeys.Contains("MemcacheSessionId"))
            {
                var cookieValue = cookie.Value;
                //var cookie1 = HttpContext.Current.Request.Cookies["MemcacheSessionId"];
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
                if(MemcacheHelper.Delete(cookieValue + "$" + key))
                {
                    return true;
                }
            }
            return true;
        }
        public bool Delete(string key)
        {
            if (HttpContext.Current.Request.Cookies.AllKeys.Contains("MemcacheSessionId"))
            {
                var cookieValue = cookie.Value;
                if (MemcacheHelper.Delete(cookieValue + "$" + key))
                {
                    return true;
                }
            }
            return true;
        }
    }
}
