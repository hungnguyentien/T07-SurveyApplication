using MediatR;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.GuiEmail.Requests.Commands;

public class GuiLaiGuiEmailCommand : IRequest<BaseCommandResponse>
{
    public CreateGuiEmailDto? GuiEmailDto { get; set; }
    public List<int> LstIdGuiMail { get; set; }
}