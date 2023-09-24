using MediatR;
using SurveyApplication.Application.DTOs.CauHoi;

namespace SurveyApplication.Application.Features.CauHoi.Requests.Queries;

public class GetConfigCauHoiRequest : IRequest<PhieuKhaoSatDto>
{
    public int IdGuiEmail { get; set; }
}