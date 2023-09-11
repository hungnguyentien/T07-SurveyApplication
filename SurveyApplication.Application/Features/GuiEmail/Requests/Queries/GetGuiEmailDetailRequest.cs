using MediatR;
using SurveyApplication.Application.DTOs.GuiEmail;

namespace SurveyApplication.Application.Features.GuiEmail.Requests.Queries;

public class GetGuiEmailDetailRequest : IRequest<GuiEmailDto>
{
    public int Id { get; set; }
}