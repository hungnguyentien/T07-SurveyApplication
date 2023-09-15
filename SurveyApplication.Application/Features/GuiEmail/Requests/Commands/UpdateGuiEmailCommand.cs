using MediatR;
using SurveyApplication.Application.DTOs.GuiEmail;

namespace SurveyApplication.Application.Features.GuiEmail.Requests.Commands;

public class UpdateGuiEmailCommand : IRequest<Unit>
{
    public UpdateGuiEmailDto? GuiEmailDto { get; set; }
}