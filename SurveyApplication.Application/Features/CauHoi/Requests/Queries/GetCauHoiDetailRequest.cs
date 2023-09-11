using MediatR;
using SurveyApplication.Application.DTOs.CauHoi;

namespace SurveyApplication.Application.Features.CauHoi.Requests.Queries;

public class GetCauHoiDetailRequest : IRequest<CauHoiDto>
{
    public int Id { get; set; }
}