using SurveyApplication.Domain.Common;

namespace SurveyApplication.Domain;

public class BangKhaoSatCauHoi : BaseDomainEntity
{
    public int IdBangKhaoSat { get; set; }
    public int IdCauHoi { get; set; }
    public int? Priority { get; set; }
    public bool? IsRequired { get; set; }
}