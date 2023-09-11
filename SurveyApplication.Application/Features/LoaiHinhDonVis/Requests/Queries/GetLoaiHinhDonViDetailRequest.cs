using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries;

public class GetLoaiHinhDonViDetailRequest : IRequest<LoaiHinhDonViDto>
{
    public int Id { get; set; }
}