using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ServiceStack.Redis;

namespace SimpleOA.Common
{
    public class RedisSet
    {
        public bool AddSet(string setId,string value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["RedisSessionId"];
            string sessionId = cookie == null ? string.Empty : cookie.ToString();
            if (!string.IsNullOrEmpty(sessionId))
            {
                using (IRedisClient redisClent = RedisHelper.clientManager.GetClient())
                {
                    redisClent.AddItemToSet(sessionId + "%" + setId, value);
                    return true;
                }
            }
            else { return false; }
        }
        public HashSet<string> GetSet(string setId)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["RedisSessionId"];
            string sessionId = cookie == null ? string.Empty : cookie.ToString();
            if (!string.IsNullOrEmpty(sessionId))
            {
                using (IRedisClient redisClent = RedisHelper.clientManager.GetClient())
                {
                    return redisClent.GetAllItemsFromSet(sessionId + "%" + setId);
                }
            }
            else
            {
                return null;
            }
        }
        public bool CheckSet(string setId,string item)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["RedisSessionId"];
            string sessionId = cookie == null ? string.Empty : cookie.ToString();
            if (!string.IsNullOrEmpty(sessionId))
            {
                using (IRedisClient redisClent = RedisHelper.clientManager.GetClient())
                {
                    return redisClent.SetContainsItem(sessionId + "%" + setId, item);
                }
            }
            else
                return false;
        }
    }
}
