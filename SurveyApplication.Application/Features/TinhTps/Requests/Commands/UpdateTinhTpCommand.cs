using MediatR;
using SurveyApplication.Application.DTOs.TinhTp;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.TinhTps.Requests.Commands;

public class UpdateTinhTpCommand : IRequest<BaseCommandResponse>
{
    public UpdateTinhTpDto? TinhTpDto { get; set; }
}