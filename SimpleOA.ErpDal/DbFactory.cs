using SimpleOA.Model.ErpModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SimpleOA.ErpDAL
{
    public class DbFactory
    {
        public static DbContext GetDbContext()
        {
            DbContext ErpDbContext = HttpContext.Current.Items["ErpDbContext"] as DbContext;
            if (ErpDbContext==null)
            {
                ErpDbContext = new ErpDataModelContext();
                HttpContext.Current.Items["ErpDbContext"] = ErpDbContext;
            }
            return ErpDbContext;
        }
    }
}
