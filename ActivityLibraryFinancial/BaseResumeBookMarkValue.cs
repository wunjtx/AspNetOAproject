using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityLibraryFinancial
{
    public class BaseResumeBookMarkValue
    {
        public object Value { get; set; }

        public int StepId { get; set; }

        public string BookMarkName { get; set; }

        public Guid InstanceId { get; set; }
    }
}
