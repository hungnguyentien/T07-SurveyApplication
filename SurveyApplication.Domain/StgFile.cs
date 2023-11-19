using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.Domain;

public class StgFile
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [MaxLength(250)] public string FileName { get; set; }

    [MaxLength(1000)] public string PhysicalPath { get; set; }

    public int FileType { get; set; } // // Avatar, văn bản, âm thanh, hình ảnh, video,...

    public string ContentType { get; set; }

    public decimal Size { get; set; }

    public int Status { get; set; } = (int)EnumCommon.ActiveFlag.Active;
}