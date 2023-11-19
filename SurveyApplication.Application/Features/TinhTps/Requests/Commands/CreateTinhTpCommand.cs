using MediatR;
using SurveyApplication.Application.DTOs.TinhTp;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.TinhTps.Requests.Commands;

public class CreateTinhTpCommand : IRequest<BaseCommandResponse>
{
    public CreateTinhTpDto? TinhTpDto { get; set; }
}