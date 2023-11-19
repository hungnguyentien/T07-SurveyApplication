using MediatR;
using Microsoft.AspNetCore.Http;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.DonVis.Requests.Commands;

public class ImportDonViCommand : IRequest<BaseCommandResponse>
{
    public IFormFile? File { get; set; }
}