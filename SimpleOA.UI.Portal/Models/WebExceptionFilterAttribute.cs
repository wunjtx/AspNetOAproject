using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.Redis;

namespace SimpleOA.UI.Portal
{
    public class WebExceptionFilterAttribute : HandleErrorAttribute
    {
        public static Queue<Exception> ExceptionQueue=new Queue<Exception>();
        public static Common.RedisQueue ExceptionRedisQueue = new Common.RedisQueue("errorMsg");
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            //cache queue method
            //ExceptionQueue.Enqueue(filterContext.Exception);
            //------------------
            //redis queue method
            ExceptionRedisQueue.EnQueue(filterContext.Exception.ToString());
        }
    }
}