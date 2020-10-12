using System;
using System.Linq;

namespace LazyDev.Core.Extensions
{
    public static class LinqExtension
    {
        public static IQueryable If(this IQueryable queryable,Func<bool> func)
        {
            if (func())
            {
                
            }

            return queryable;
        }
    }
}
