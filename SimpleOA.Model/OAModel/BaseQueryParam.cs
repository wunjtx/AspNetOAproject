using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOA.Model.OAModel
{
    public class BaseQueryParam
    {
        public int pageIndex { get; set; }
        public int pageCount { get; set; }
        public int total { get; set; }
    }
}
