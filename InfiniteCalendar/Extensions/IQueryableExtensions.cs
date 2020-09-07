using System;
using System.Collections.Generic;
using System.Linq;

using InfiniteCalendar.Parameters;

namespace InfiniteCalendar.Extensions
{
    public static class MyExtensions
    {
        public static IQueryable<T> getPage<T>(this IOrderedQueryable<T> query, PaginationParameters pagination)
        {
            return query.Skip(pagination.itemsToSkip).Take(pagination.pageSize);
        }
    }
}
