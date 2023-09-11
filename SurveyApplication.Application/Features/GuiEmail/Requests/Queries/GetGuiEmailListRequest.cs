using MediatR;
using SurveyApplication.Application.DTOs.GuiEmail;

namespace SurveyApplication.Application.Features.GuiEmail.Requests.Queries;

public class GetGuiEmailListRequest : IRequest<List<GuiEmailDto>>
{
}