using MediatR;
using SurveyApplication.Application.DTOs.LinhVucHoatDong;

namespace SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Queries;

public class GetLinhVucHoatDongDetailRequest : IRequest<LinhVucHoatDongDto>
{
    public int Id { get; set; }
}