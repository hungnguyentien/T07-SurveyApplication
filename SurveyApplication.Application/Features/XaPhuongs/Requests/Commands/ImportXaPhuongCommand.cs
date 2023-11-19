using MediatR;
using Microsoft.AspNetCore.Http;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.XaPhuongs.Requests.Commands;

public class ImportXaPhuongCommand : IRequest<BaseCommandResponse>
{
    public IFormFile? File { get; set; }
}