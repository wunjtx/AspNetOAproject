using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOA.Common
{
    public static class ExtEnumToInt
    {
        public static int ToInt(this System.Enum e)
        {
            return e.GetHashCode();
        }
    }
    public static class ExtStringToInt
    {
        public static int ToInt(this string s, int defaultValue = 0)
        {
            int i;
            return int.TryParse(s, out i) ? i : defaultValue;
        }
    }
}
