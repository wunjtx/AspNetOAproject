using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOA.Common
{
    public class SerializerHelper
    {
        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializerToString(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        /// <summary>
        /// Deserialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T DeserializeToObject<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}
