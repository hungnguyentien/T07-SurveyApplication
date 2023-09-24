namespace SurveyApplication.Domain.Common
{
    public class SurveyConfiguration
    {
        public string CustomerCode { get; set; }
        public string BuildNumber { get; set; }
        public string Env { get; set; }
        public bool AutoMigration { get; set; }
        public bool UseDebugMode { get; set; }
    }
}
