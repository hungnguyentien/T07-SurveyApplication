using MediatR;
using SurveyApplication.Application.DTOs.XaPhuong;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.XaPhuongs.Requests.Commands;

public class CreateXaPhuongCommand : IRequest<BaseCommandResponse>
{
    public CreateXaPhuongDto? XaPhuongDto { get; set; }
}