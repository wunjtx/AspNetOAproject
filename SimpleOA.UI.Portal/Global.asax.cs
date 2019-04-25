using FineUIMvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SimpleOA.UI.Portal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(typeof(JArray), new JArrayModelBinder());
            ModelBinders.Binders.Add(typeof(JObject), new JObjectModelBinder());

            Common.ILogger logWriter = new Common.LogToLocal();

            ThreadPool.QueueUserWorkItem(u => {
                while (true)
                {
                    //cache queue method
                    //if (WebExceptionFilterAttribute.ExceptionQueue!=null && WebExceptionFilterAttribute.ExceptionQueue.Count()>0)
                    //------------------
                    //redis queue method
                    if (WebExceptionFilterAttribute.ExceptionRedisQueue != null && WebExceptionFilterAttribute.ExceptionRedisQueue.Count() > 0)
                    {
                        //cache queue method
                        //string errorMsg = WebExceptionFilterAttribute.ExceptionQueue.Dequeue().ToString();
                        //string errorMsg = ex.Message;
                        //------------------
                        //redis queue method
                        string errorMsg = WebExceptionFilterAttribute.ExceptionRedisQueue.Dequeue().ToString();
                        if (!string.IsNullOrEmpty(errorMsg))
                        {
                            logWriter.MsgLogger("WebLogger", errorMsg);
                        }
                        Thread.Sleep(1000);
                    }
                    else
                    { 
                        Thread.Sleep(2000);
                    }
                }
            });
        }
    }
}
