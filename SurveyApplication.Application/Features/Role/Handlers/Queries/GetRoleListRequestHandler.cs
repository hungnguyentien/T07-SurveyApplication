using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.Role;
using SurveyApplication.Application.Features.Role.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.Role.Handlers.Queries;

public class GetRoleListRequestHandler : BaseMasterFeatures, IRequestHandler<GetRoleListRequest, List<RoleDto>>
{
    private readonly IMapper _mapper;

    public GetRoleListRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<List<RoleDto>> Handle(GetRoleListRequest request, CancellationToken cancellationToken)
    {
        var role = await _surveyRepo.Role.GetAllListAsync(x => !x.Deleted);
        return _mapper.Map<List<RoleDto>>(role);
    }
}