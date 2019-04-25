using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOA.Common
{
    public class UserIp
    {
        public string Ip { get; set; }
        public DateTime FirstAttempt { get; set; }
        public DateTime LastAttempt { get; set; }
        public int AttemptTime { get; set; }
    }
}
