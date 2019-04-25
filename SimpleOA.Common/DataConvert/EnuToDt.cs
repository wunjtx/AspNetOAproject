using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOA.Common
{
    public static class EnuToDt
    {
        public static DataTable ToTable<T>(this IEnumerable<T> collection)
        {
            var props = typeof(T).GetProperties();
            var func = GetGetDelegate<T>(props);
            var dt = new DataTable();
            dt.Columns.AddRange(
                props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray()
            );
            collection.ToList().ForEach(i => dt.Rows.Add(func(i)));

            return dt;
        }

        static Func<T, object[]> GetGetDelegate<T>(PropertyInfo[] ps)
        {
            var param_obj = Expression.Parameter(typeof(T), "obj");
            Expression newArrayExpression = Expression.NewArrayInit(typeof(object), ps.Select(p => Expression.Property(param_obj, p)));
            return Expression.Lambda<Func<T, object[]>>(newArrayExpression, param_obj).Compile();
        }

    }
}
