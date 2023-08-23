using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.LoaiHinhDonVi
{
    public interface ILoaiHinhDonViDto
    {
        public string MaLoaiHinh { get; set; }
        public string TenLoaiHinh { get; set; }
        public string MoTa { get; set; }
    }
}
