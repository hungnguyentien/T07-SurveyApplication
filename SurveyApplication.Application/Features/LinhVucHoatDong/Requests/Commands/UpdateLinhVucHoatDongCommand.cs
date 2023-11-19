using MediatR;
using SurveyApplication.Application.DTOs.LinhVucHoatDong;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Commands;

public class UpdateLinhVucHoatDongCommand : IRequest<BaseCommandResponse>
{
    public UpdateLinhVucHoatDongDto? LinhVucHoatDongDto { get; set; }
}