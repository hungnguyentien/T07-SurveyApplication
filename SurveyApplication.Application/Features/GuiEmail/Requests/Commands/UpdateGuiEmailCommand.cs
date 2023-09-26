using MediatR;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.GuiEmail.Requests.Commands;

public class UpdateGuiEmailCommand : IRequest<BaseCommandResponse>
{
    public UpdateGuiEmailDto? GuiEmailDto { get; set; }
}