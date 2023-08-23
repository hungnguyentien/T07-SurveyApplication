using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries
{
    public class GetLoaiHinhDonViDetailRequest : IRequest<LoaiHinhDonViDto>
    {
        public string? MaLoaiHinh { get; set; }
    }
}
