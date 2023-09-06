using MediatR;
using SurveyApplication.Application.DTOs.LinhVucHoatDong;

namespace SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Queries
{
    public class GetLinhVucHoatDongAllRequest : IRequest<List<LinhVucHoatDongDto>>
    {
    }
}
