using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOA.Common
{
    public class CheckUserIpByMemcache
    {
        public bool CheckIpAttemptTimes(int maxAtttemptTimes,int expiredMinutes)
        {
            GetUserIp GetIp = new GetUserIp();
            string userIp = GetIp.GetLoginIp();
            if( MemcacheHelper.Get(userIp)!=null)
            {
                UserIp uip = SerializerHelper.DeserializeToObject<UserIp>( MemcacheHelper.Get(userIp).ToString());
                if (uip.AttemptTime++> maxAtttemptTimes)
                {
                    return false;
                }
                else
                {
                    uip.AttemptTime++;
                    uip.LastAttempt = DateTime.Now;
                    if (uip.LastAttempt.Subtract(uip.FirstAttempt).TotalMinutes> expiredMinutes)
                    {
                        uip.FirstAttempt = uip.LastAttempt;
                        uip.AttemptTime = 1;
                        MemcacheHelper.Set(userIp, SerializerHelper.SerializerToString(uip), uip.FirstAttempt.AddMinutes(expiredMinutes));
                    }
                    else { 
                    MemcacheHelper.Set(userIp, SerializerHelper.SerializerToString(uip),uip.FirstAttempt.AddMinutes(expiredMinutes));
                    }
                    return true;
                }
            }
            else
            {
                UserIp uip = new UserIp();
                uip.Ip = userIp;
                uip.FirstAttempt = DateTime.Now;
                uip.LastAttempt = uip.FirstAttempt;
                uip.AttemptTime = 1;
                MemcacheHelper.Set(userIp, SerializerHelper.SerializerToString(uip), uip.FirstAttempt.AddMinutes(expiredMinutes));
                return true;
            }
        }

    }
}
