using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOA.Common
{
    public interface ILogger
    {
        void MsgLogger(string logger, string msg);
    }
}
