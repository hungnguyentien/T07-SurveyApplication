using Microsoft.AspNetCore.Http;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.Application.DTOs.StgFile
{
    public class StgFileDto
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public byte[] FileContents { get; set; }
        public string PhysicalPath { get; set; }
        public int FileType { get; set; }
        public string ContentType { get; set; }
        public decimal Size { get; set; }
        public int Status { get; set; } = (int)EnumCommon.ActiveFlag.Active;
    }
}
