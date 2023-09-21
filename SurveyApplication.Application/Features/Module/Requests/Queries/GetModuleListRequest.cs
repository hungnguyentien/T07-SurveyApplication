using MediatR;
using SurveyApplication.Application.DTOs.Module;

namespace SurveyApplication.Application.Features.Module.Requests.Queries;

public class GetModuleListRequest : IRequest<List<ModuleDto>>
{
}