using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace SimpleOA.Common
{
    public class RedisQueue
    {
        private RedisHelper rp = new RedisHelper();
        public RedisQueue(string queueName)
        {
            rp.QueueName = queueName;
        }
        public  void EnQueue(string value)
        {
            using (IRedisClient redisClent = RedisHelper.clientManager.GetClient())
            {
                redisClent.EnqueueItemOnList(rp.QueueName, value);
            }
        }
        public  string Dequeue()
        {
            using (IRedisClient redisClent = RedisHelper.clientManager.GetClient())
            {
                return redisClent.DequeueItemFromList(rp.QueueName);
            }
        }
        public int Count()
        {
            using (IRedisClient redisClent = RedisHelper.clientManager.GetClient())
            {
                return redisClent.GetListCount(rp.QueueName);
            }
        }
    }
    public class RedisQueue<T> 
    {
        private RedisHelper rp = new RedisHelper();
        public RedisQueue(string queueName)
        {
            rp.QueueName = queueName;
        }
        public void EnQueue(T value)
        {
            using (IRedisClient redisClent = RedisHelper.clientManager.GetClient())
            {
                redisClent.EnqueueItemOnList(rp.QueueName, SerializerHelper.SerializerToString(value));
            }
        }
        public T Dequeue()
        {
            using (IRedisClient redisClent = RedisHelper.clientManager.GetClient())
            {
                return SerializerHelper.DeserializeToObject<T>(redisClent.DequeueItemFromList(rp.QueueName));
            }
        }
        public int Count()
        {
            using (IRedisClient redisClent = RedisHelper.clientManager.GetClient())
            {
                return redisClent.GetListCount(rp.QueueName);
            }
        }
    }
}
