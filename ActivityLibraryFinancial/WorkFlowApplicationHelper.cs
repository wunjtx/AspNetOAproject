using System;
using System.Activities;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Activities.DurableInstancing;
using System.Runtime.DurableInstancing;

namespace ActivityLibraryFinancial
{
    public class WorkflowApplicationHelper
    {

        private static readonly string StrSql;
        
        static WorkflowApplicationHelper()
        {
            StrSql = ConfigurationManager.ConnectionStrings["wfsql"].ConnectionString;
        }

        public static WorkflowApplication CreateWorkflowApp(Activity activity, Dictionary<string, object> dicParam)
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            WorkflowApplication wfApp;
            if (dicParam == null)
            {
                wfApp = new WorkflowApplication(activity);
            }
            else
            {
                wfApp = new WorkflowApplication(activity, dicParam);
            }
            wfApp.Idle += a =>//当工作流停下来的时候，执行此事件响应方法。
            {
                //Console.WriteLine("工作流停下来了...");
                //Common.LogHelper.WriteLog("工作流停下来了");
            };

            //当咱们工作流停顿下来的时候，进行什么操作。如果返回是Unload。那么就卸载当前
            //工作流实例，持久化到数据库里面去。
            wfApp.PersistableIdle = delegate (WorkflowApplicationIdleEventArgs e2)
            {
                //Console.WriteLine("工作卸载进行持久化,书签被创建时候就会执行序列化到数据库里面去。");
                //Common.LogHelper.WriteLog("工作卸载进行持久化");
                autoResetEvent.Set();
                return PersistableIdleAction.Unload;
            };

            wfApp.Unloaded += a =>
            {
                autoResetEvent.Set();
                //Console.WriteLine("工作流被卸载");
                //Common.LogHelper.WriteLog("工作流被卸载");
            };
            wfApp.OnUnhandledException += a =>
            {
                //autoResetEvent.Set();

                //Console.WriteLine("出现了异常..");
                //Common.LogHelper.WriteLog(a.UnhandledException.ToString());
                return UnhandledExceptionAction.Terminate;//当出现未处理的异常的时候，直接结束工作流
            };

            wfApp.Aborted += a =>
            {
                autoResetEvent.Set();
                //Console.WriteLine("Aborted");
            };

            //创建一个保存 工作流实例的sqlstore对象。
            SqlWorkflowInstanceStore store = new SqlWorkflowInstanceStore(StrSql);

            InstanceView view = store.Execute(store.CreateInstanceHandle(), new CreateWorkflowOwnerCommand(), TimeSpan.FromSeconds(30));
            store.DefaultInstanceOwner = view.InstanceOwner;

            //wfApp在进行持久化的时候，保存到上面对象配置的数据库里面去。
            wfApp.InstanceStore = store;
            wfApp.Run();
            autoResetEvent.WaitOne();
            return wfApp;
        }

        public static WorkflowApplication ResumeBookMark(Activity activity, Guid instanceId, string bookmarkName, object value)
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            WorkflowApplication wfApp = new WorkflowApplication(activity);
            wfApp.Idle += a =>//当工作流停下来的时候，执行此事件响应方法。
            {
                //Console.WriteLine("工作流停下来了...");
            };

            //当咱们工作流停顿下来的时候，进行什么操作。如果返回是Unload。那么就卸载当前
            //工作流实例，持久化到数据库里面去。
            wfApp.PersistableIdle = delegate (WorkflowApplicationIdleEventArgs e3)
            {
                //Console.WriteLine("工作卸载进行持久化");
                autoResetEvent.Set();
                return PersistableIdleAction.Unload;
            };

            wfApp.Unloaded += a =>
            {
                //Common.LogHelper.WriteLog("工作流被卸载");
                autoResetEvent.Set();
                //Console.WriteLine("工作流被卸载");
            };
            wfApp.OnUnhandledException += a =>
            {
                //autoResetEvent.Set();
                //LogHelper.WriteLog(a.ExceptionSource.ToString());
                //Console.WriteLine("出现了异常..");
                return UnhandledExceptionAction.Terminate;//当出现未处理的异常的时候，直接结束工作流
            };

            wfApp.Aborted += a =>
            {
                autoResetEvent.Set(); //Console.WriteLine("Aborted");
            };

            //创建一个保存 工作流实例的sqlstore对象。
            SqlWorkflowInstanceStore store = new SqlWorkflowInstanceStore(StrSql);

            //wfApp在进行持久化的时候，保存到上面对象配置的数据库里面去。
            wfApp.InstanceStore = store;

            //从数据库中加载当前工作流实例的状态。
            wfApp.Load(instanceId);
            
            //让工作流沿着 Demo书签位置继续往下执行。
            wfApp.ResumeBookmark(bookmarkName, value);
            autoResetEvent.WaitOne();
            return wfApp;
        }

    }
}
