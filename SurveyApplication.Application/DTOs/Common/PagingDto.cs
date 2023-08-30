namespace SurveyApplication.Application.DTOs.Common
{
    public class PagingDto<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalFilter { get; set; }
        public T Param { get; set; }

        public PagingDto(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalFilter = count;
            AddRange(items);
        }
        public PagingDto(List<T> items)
        {
            PageIndex = 1;
            TotalPages = 1;
            PageSize = items.Count;
            TotalFilter = items.Count;
            AddRange(items);
        }

        public PagingDto()
        {
            PageIndex = 1;
            TotalPages = 1;
            PageSize = 0;
            TotalFilter = 0;
        }
    }
}
