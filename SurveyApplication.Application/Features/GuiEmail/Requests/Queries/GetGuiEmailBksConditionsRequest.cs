using MediatR;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.GuiEmail.Requests.Queries;

public class GetGuiEmailBksConditionsRequest : IRequest<BaseQuerieResponse<GuiEmailBksDto>>
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public string? Keyword { get; set; }
}