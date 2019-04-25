using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ServiceStack.Redis;

namespace SimpleOA.Common
{
    public class RedisSession
    {
        //public RedisSession(string sessionId)
        //{
        //    rp.SessionId = sessionId;
        //}
        public void CreateSessionId(string SessionId)
        {
            using(IRedisClient redisClient = RedisHelper.clientManager.GetClient())
            {
                HttpCookie cookie1 = new HttpCookie("RedisSessionId");
                cookie1.Value = SessionId;
                HttpContext.Current.Response.Cookies.Add(cookie1);
                redisClient.Set<string>(SessionId + "$" + "RedisSessionId", SessionId);
            }
        }
        public string GetSessionId()
        {
            using (IRedisClient redisClient = RedisHelper.clientManager.GetClient())
            {
                string SessionId = HttpContext.Current.Request.Cookies["RedisSessionId"].Value;
                if(!string.IsNullOrEmpty( redisClient.Get<string>(SessionId + "$" + "RedisSessionId")))
                {
                    return SessionId;
                }
                else
                {
                    HttpCookie cookie1 = new HttpCookie("RedisSessionId");
                    cookie1.Value = Guid.NewGuid().ToString();
                    HttpContext.Current.Response.Cookies.Add(cookie1);
                    redisClient.Set<string>(cookie1.Value + "$" + "RedisSessionId", cookie1.Value);
                    return cookie1.Value;
                }
            }
        }
        public bool SetItem<T>(string sessionKey,T value,string SessionId)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["RedisSessionId"];
            using (IRedisClient redisClent = RedisHelper.clientManager.GetClient())
            {
                if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
                {
                    if (redisClent.Set<string>(cookie.Value + "$" + sessionKey, Common.SerializerHelper.SerializerToString(value)))
                        return true;
                    else
                        return false;
                }
                else
                {
                    HttpCookie cookie1 = new HttpCookie("RedisSessionId");
                    cookie1.Value = SessionId;
                    HttpContext.Current.Response.Cookies.Add(cookie1);
                    if (!string.IsNullOrEmpty(SessionId))
                    {
                        if (redisClent.Set<string>(SessionId + "$" + "RedisSessionId", SessionId))
                        {
                            if (redisClent.Set<string>(SessionId + "$" + sessionKey, Common.SerializerHelper.SerializerToString(value)))
                                return true;
                        }
                    }
                    return false;
                }
            }
        }
        public  T GetItem<T>(string sessionKey)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["RedisSessionId"];
            string SessionId = cookie == null ? string.Empty : cookie.Value;
            if (!string.IsNullOrEmpty(SessionId))
            {
                using (IRedisClient redisClent = RedisHelper.clientManager.GetClient())
                {
                    //T value = redisClent.Get<T>(SessionId + "$" + sessionKey);
                    //if (!EqualityComparer<T>.Default.Equals(default(T), value))
                    //{
                    //    return value;
                    //}
                    //else
                    //{
                    //    return default(T);
                    //}
                    string Value = redisClent.Get<string>(SessionId + "$" + sessionKey);
                    if (!string.IsNullOrEmpty(Value))
                    {
                       if(!EqualityComparer<T>.Default.Equals(default(T), SerializerHelper.DeserializeToObject<T>(Value)))
                        {
                            return  SerializerHelper.DeserializeToObject<T>(Value);
                        }
                        else
                        {
                            return  default(T);
                        }
                    }
                    else
                    {
                        return  default(T);
                    }
                }
            }
            else
            {
                return  default(T);
            }
        }
        public bool DeleteSession()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["RedisSessionId"];
            if (HttpContext.Current.Request.Cookies.AllKeys.Contains("RedisSessionId"))
            {
                if (cookie != null && cookie.Value != null)
                {
                    var cookieValue = cookie.Value;
                    //var cookie1 = HttpContext.Current.Request.Cookies["MemcacheSessionId"];
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    return true;
                }
            }
            return true;
        }
        //public bool DeleteValue(string sessionId)
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies["RedisSessionId"];
        //    if (HttpContext.Current.Request.Cookies.AllKeys.Contains("RedisSessionId"))
        //    {
        //        if (cookie != null && cookie.Value != null)
        //        {
        //            using (IRedisClient redisClent = RedisHelper.clientManager.GetClient())
        //            {
        //                var cookieValue = cookie.Value;
        //                //var cookie1 = HttpContext.Current.Request.Cookies["MemcacheSessionId"];
        //                cookie.Expires = DateTime.Now.AddDays(-1);
        //                HttpContext.Current.Response.Cookies.Add(cookie);
        //                redisClent.Remove(cookie.Value.ToString() + "$" + sessionId);
        //                return true;
        //            }
        //        }
        //    }
        //    return true;
        //}
        public bool Delete(string sessionId)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["RedisSessionId"];
            if (HttpContext.Current.Request.Cookies.AllKeys.Contains("RedisSessionId"))
            {
                if (cookie != null && cookie.Value != null)
                {
                    using (IRedisClient redisClent = RedisHelper.clientManager.GetClient())
                    {
                        var cookieValue = cookie.Value;
                        redisClent.Remove(cookie.Value.ToString() + "$" + sessionId);
                        return true;
                    }
                }
            }
            return true;
        }
    }
}
