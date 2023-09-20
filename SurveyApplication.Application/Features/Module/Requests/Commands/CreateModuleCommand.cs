using MediatR;
using SurveyApplication.Application.DTOs.Module;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.Module.Requests.Commands;

public class CreateModuleCommand : IRequest<BaseCommandResponse>
{
    public CreateModuleDto? ModuleDto { get; set; }
}