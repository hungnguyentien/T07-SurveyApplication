using MediatR;
using Microsoft.AspNetCore.Http;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.TinhTps.Requests.Commands;

public class ImportTinhTpCommand : IRequest<BaseCommandResponse>
{
    public IFormFile? File { get; set; }
}