namespace SurveyApplication.Domain.Common
{
    public class Paging<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }
        public T Param { get; set; }

        public Paging(List<T> items, long count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }
        public Paging(List<T> items)
        {
            PageIndex = 1;
            TotalPages = 1;
            PageSize = items.Count;
            TotalCount = items.Count;
            AddRange(items);
        }

        public Paging()
        {
            PageIndex = 1;
            TotalPages = 1;
            PageSize = 0;
            TotalCount = 0;
        }
    }
}
