namespace SurveyApplication.Domain.Common.Responses
{
    public class PageCommandResponse<T> where T : class
    {
        public List<T> Data { get; set; }
        public int TotalFilter { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string Keyword { get; set; }
    }
}
