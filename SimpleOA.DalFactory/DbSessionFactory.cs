using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SimpleOA.DalFactory
{
    public class DbSessionFactory
    {
        public static IDbSession GetDbSession() 
        {
            IDbSession dbSession = HttpContext.Current.Items["DbSession"] as IDbSession;
            if (dbSession==null)
            {
                dbSession = new DbSession();
                HttpContext.Current.Items["DbSession"] = dbSession;
            }
            return dbSession;
        }
        public static IDbSession GetCurrentDbSessionByCallContext()
        {
            IDbSession db = CallContext.GetData("DbSession") as IDbSession;
            if (db == null)
            {
                db = new DbSession();
                CallContext.SetData("DbSession", db);
            }
            return db;
        }
    }
}
