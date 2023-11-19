using MediatR;
using SurveyApplication.Application.DTOs.BaoCaoCauHoi;

namespace SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Queries;

public class GetDashBoardRequest : IRequest<DashBoardDto>
{
    public DateTime? NgayBatDau { get; set; }
    public DateTime? NgayKetThuc { get; set; }
}