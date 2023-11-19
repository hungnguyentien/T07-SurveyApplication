using MediatR;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.GuiEmail.Requests.Queries;

public class GetGuiEmailBksDetailRequest : IRequest<BaseQuerieResponse<GuiEmailDto>>
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public int IdBanhgKhaoSat { get; set; }
    public int? TrangThaiGuiEmail { get; set; }
}