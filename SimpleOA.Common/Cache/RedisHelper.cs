using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOA.Common
{
    public class RedisHelper
    {
        public static IRedisClientsManager clientManager = new PooledRedisClientManager(System.Configuration.ConfigurationManager.AppSettings["RedisConfig"].Split(','));
        //public static IRedisClient redisClent = clientManager.GetClient();
        public string QueueName;
        public string SetName;
        public string SessionId;
    }
}
