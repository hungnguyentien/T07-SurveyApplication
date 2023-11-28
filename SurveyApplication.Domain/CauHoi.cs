﻿using System.ComponentModel.DataAnnotations;
using SurveyApplication.Domain.Common;

namespace SurveyApplication.Domain;

public class CauHoi : BaseDomainEntity
{
    [Required] [MaxLength(250)] public string MaCauHoi { get; set; }

    public short LoaiCauHoi { get; set; }

    [Required] public string TieuDe { get; set; }

    public string NoiDung { get; set; }
    public int SoLuongFileToiDa { get; set; }
    public int KichThuocFile { get; set; }
    public bool? IsOther { get; set; }
    public string LabelCauTraLoi { get; set; }
}