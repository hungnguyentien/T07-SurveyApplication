namespace SurveyApplication.Domain.Common.Responses
{
    public class BaseQuerieResponse<T>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Keyword { get; set; } = "";
        public long TotalFilter { get; set; }
        public List<T>? Data { get; set; }
    }
}
