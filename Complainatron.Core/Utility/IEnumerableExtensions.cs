using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Complainatron.Core.Utility
{
    public static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }
    }
}
