using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOA.Common
{
    public class LogToLocal:ILogger
    {
        public void MsgLogger(string logger ,string msg)
        {
            ILog log = log4net.LogManager.GetLogger(logger);
            log.Error(msg);
        }
    }
}
