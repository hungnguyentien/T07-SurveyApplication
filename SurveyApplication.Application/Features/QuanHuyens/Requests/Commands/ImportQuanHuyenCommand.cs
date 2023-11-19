using MediatR;
using Microsoft.AspNetCore.Http;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.QuanHuyens.Requests.Commands;

public class ImportQuanHuyenCommand : IRequest<BaseCommandResponse>
{
    public IFormFile? File { get; set; }
}