using System;
using System.Collections.Generic;

namespace Hangfire.Domain.Models
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; set; }
        public int TotalFilter { get; set; }
        public T Param { get; set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalFilter = count;
            AddRange(items);
        }
        public PaginatedList(List<T> items)
        {
            PageIndex = 1;
            TotalPages = 1;
            PageSize = items.Count;
            TotalFilter = items.Count;
            AddRange(items);
        }
        public PaginatedList()
        {
            PageIndex = 1;
            TotalPages = 1;
            PageSize = 0;
            TotalFilter = 0;
        }
    }
}
