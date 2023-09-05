namespace SurveyApplication.API.Models
{
    public class Paging
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Keyword { get; set; } = "";
    }
}
