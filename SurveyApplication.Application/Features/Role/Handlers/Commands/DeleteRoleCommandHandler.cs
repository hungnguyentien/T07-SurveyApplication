using MediatR;
using Microsoft.AspNetCore.Identity;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.Role.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.Role.Handlers.Commands;

public class DeleteRoleCommandHandler : BaseMasterFeatures, IRequestHandler<DeleteRoleCommand, BaseCommandResponse>
{
    private readonly RoleManager<Domain.Role> _roleManager;

    public DeleteRoleCommandHandler(ISurveyRepositoryWrapper surveyRepository, RoleManager<Domain.Role> roleManager) :
        base(
            surveyRepository)
    {
        _roleManager = roleManager;
    }

    public async Task<BaseCommandResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();

        var lstRole = await _surveyRepo.Role.GetByIds(x => request.Ids.Contains(x.Id));

        if (lstRole == null || lstRole.Count == 0)
            throw new NotFoundException(nameof(Role), request.Ids);

        foreach (var item in lstRole)
            item.Deleted = true;

        await _surveyRepo.Role.UpdateAsync(lstRole);
        await _surveyRepo.SaveAync();
        return new BaseCommandResponse("Xóa thành công!");
    }
}