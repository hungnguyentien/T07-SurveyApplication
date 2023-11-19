using System.ComponentModel.DataAnnotations;
using SurveyApplication.Domain.Common;

namespace SurveyApplication.Domain;

public class XaPhuong : BaseDomainEntity
{
    [MaxLength(250)] public string Code { get; set; }

    [MaxLength(250)] public string ParentCode { get; set; }

    [MaxLength(250)] public string Name { get; set; }

    [MaxLength(250)] public string Type { get; set; }
}