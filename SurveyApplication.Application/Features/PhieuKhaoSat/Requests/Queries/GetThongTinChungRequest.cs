using MediatR;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Queries;

public class GetThongTinChungRequest : IRequest<ThongTinChungDto>
{
    public int IdGuiEmail { get; set; }
}