using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries
{
    public class GetLoaiHinhDonViDetailRequest : IRequest<LoaiHinhDonViDto>
    {
        [Required(ErrorMessage = "Mã loại hình không được để trống")]
        public string? MaLoaiHinh { get; set; }
    }
}
