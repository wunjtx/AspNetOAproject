using SimpleOA.Model.OAModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SimpleOA.OADAL
{
    public class DbFactory
    {
        public static DbContext GetDbContext()
        {
            DbContext OADbContext = HttpContext.Current.Items["OADbContext"] as DbContext;
            if (OADbContext == null)
            {
                OADbContext = new OADataModelContext();
                HttpContext.Current.Items["OADbContext"] = OADbContext;
            }
            return OADbContext;
            //return new Model.OADataModelContext();

        }
        public static DbContext GetDbContextByCallContext()
        {
            //一次请求共用一个实例。 上下文都可以做到切换。
            //return new DataModelContainer();
            DbContext db = CallContext.GetData("DbContext") as DbContext;
            if (db == null)
            {
                db = new OADataModelContext();
                CallContext.SetData("DbContext", db);
            }
            return db;
        }
    }
}
