using MediatR;
using SurveyApplication.Application.DTOs.Module;

namespace SurveyApplication.Application.Features.Module.Requests.Queries;

public class GetModuleDetailRequest : IRequest<ModuleDto>
{
    public int Id { get; set; }
}