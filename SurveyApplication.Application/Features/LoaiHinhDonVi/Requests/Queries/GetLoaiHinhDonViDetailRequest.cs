using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;

namespace SurveyApplication.Application.Features.LoaiHinhDonVi.Requests.Queries;

public class GetLoaiHinhDonViDetailRequest : IRequest<LoaiHinhDonViDto>
{
    public int Id { get; set; }
}