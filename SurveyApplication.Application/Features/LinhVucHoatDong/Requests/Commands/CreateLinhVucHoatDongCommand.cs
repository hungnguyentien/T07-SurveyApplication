using MediatR;
using SurveyApplication.Application.DTOs.LinhVucHoatDong;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Commands;

public class CreateLinhVucHoatDongCommand : IRequest<BaseCommandResponse>
{
    public CreateLinhVucHoatDongDto? LinhVucHoatDongDto { get; set; }
}