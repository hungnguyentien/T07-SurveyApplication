using SurveyApplication.Utility.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

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
