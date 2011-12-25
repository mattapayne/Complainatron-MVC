using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Complainatron.Core.Extensions
{
    public static class GoogleChartExtensions
    {
        private static IDictionary<Type, string> typeMap = new Dictionary<Type, string>() { 
                { typeof(string), "string" },
                { typeof(Int32), "number" }
            };

        public static string ToChartJson<T>(this IEnumerable<T> items, Func<T, string> textResolver, Func<T, object> valueResolver)
        {
            var rows = new List<object>();

            var properties = typeof(T).GetProperties();

            var columns = properties.Select(p => new { id = p.Name.ToLower(), label = p.Name, type = typeMap[p.PropertyType] }).ToList();

            foreach (var t in items)
            {
                rows.Add(new
                {
                    c = new object[] {
                        new {
                            v = textResolver.Invoke(t)
                        },
                        new {
                            v = valueResolver.Invoke(t)
                        }
                    }
                });
            }

            var o = new
            {
                cols = columns,
                rows = rows
            };

            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(o);
        }
    }
}
