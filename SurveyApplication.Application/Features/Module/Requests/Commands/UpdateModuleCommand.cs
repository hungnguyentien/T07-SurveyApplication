using MediatR;
using SurveyApplication.Application.DTOs.Module;

namespace SurveyApplication.Application.Features.Module.Requests.Commands;

public class UpdateModuleCommand : IRequest<Unit>
{
    public UpdateModuleDto? ModuleDto { get; set; }
}