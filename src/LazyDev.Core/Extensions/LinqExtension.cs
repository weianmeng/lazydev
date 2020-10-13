using System;
using System.Linq;
using System.Linq.Expressions;
using LazyDev.Core.Common;

namespace LazyDev.Core.Extensions
{
    public static class LinqExtension
    {
        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int skipCount, int pageSize)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            return query.Skip(skipCount).Take(pageSize);
        }


        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, IPageRequest pageRequest)
        {
            return query.PageBy(pageRequest.Skip, pageRequest.PageSize);
        }



        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition ? query.Where(predicate) : query;
        }

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, int, bool>> predicate)
        {
            return condition
                ? query.Where(predicate)
                : query;
        }
    }
}
