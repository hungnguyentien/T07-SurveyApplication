using MediatR;
using SurveyApplication.Application.DTOs.XaPhuong;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.XaPhuongs.Requests.Commands;

public class UpdateXaPhuongCommand : IRequest<BaseCommandResponse>
{
    public UpdateXaPhuongDto? XaPhuongDto { get; set; }
}