using MediatR;
using SurveyApplication.Application.DTOs.LinhVucHoatDong;

namespace SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Queries
{
    public class GetLinhVucHoatDongListRequest : IRequest<List<LinhVucHoatDongDto>>
    {
    }
}
