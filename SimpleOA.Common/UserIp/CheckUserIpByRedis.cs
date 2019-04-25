using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOA.Common
{
    public class CheckUserIpByRedis
    {
        //RedisSession rs = new RedisSession();

        public bool CheckIpAttemptTimes(int maxAtttemptTimes, int expiredMinutes)
        {
            bool result = false;
            UserIp uip;
            GetUserIp GetIp = new GetUserIp();
            string userIp = GetIp.GetLoginIp();
            using (IRedisClient redisClent = RedisHelper.clientManager.GetClient())
            {
                if (redisClent.Get<UserIp>(userIp) != null)
                {
                    uip = redisClent.Get<UserIp>(userIp);
                    if (uip.AttemptTime++ > maxAtttemptTimes)
                    {
                        result = false;
                    }
                    else
                    {
                        uip.AttemptTime++;
                        uip.LastAttempt = DateTime.Now;
                        if (uip.LastAttempt.Subtract(uip.FirstAttempt).TotalMinutes > expiredMinutes)
                        {
                            uip.FirstAttempt = uip.LastAttempt;
                            uip.AttemptTime = 1;
                            redisClent.Set<UserIp>(userIp, uip);
                        }
                        else
                        {
                            redisClent.Set<UserIp>(userIp, uip);
                        }
                        result = true;
                    }
                }
                else
                {
                    uip = new UserIp();
                    uip.Ip = userIp;
                    uip.FirstAttempt = DateTime.Now;
                    uip.LastAttempt = uip.FirstAttempt;
                    uip.AttemptTime = 1;
                    redisClent.Set<UserIp>(userIp, uip);
                    result = true;
                }
            }
            return result;
        }

    }
}

