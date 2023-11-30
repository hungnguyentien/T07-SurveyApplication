using SurveyApplication.Domain.Common;

namespace SurveyApplication.Domain
{
    public class LogNhanMail : BaseDomainEntity
    {
        public string MaDoanhNghiep { get; set; }
        public string Data { get; set; }
    }
}
