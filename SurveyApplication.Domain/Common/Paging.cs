namespace SurveyApplication.Domain.Common;

public class Paging<T> : List<T>
{
    public Paging(List<T> items, long count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        PageSize = pageSize;
        TotalFilter = count;
        AddRange(items);
    }

    public Paging(List<T> items)
    {
        PageIndex = 1;
        TotalPages = 1;
        PageSize = items.Count;
        TotalFilter = items.Count;
        AddRange(items);
    }

    public Paging()
    {
        PageIndex = 1;
        TotalPages = 1;
        PageSize = 0;
        TotalFilter = 0;
    }

    public int PageIndex { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public long TotalFilter { get; set; }
    public T Param { get; set; }
}