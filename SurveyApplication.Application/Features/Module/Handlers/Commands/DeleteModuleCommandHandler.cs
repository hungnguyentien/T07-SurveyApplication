using AutoMapper;
using MediatR;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.Module.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.Module.Handlers.Commands;

public class DeleteModuleCommandHandler : BaseMasterFeatures, IRequestHandler<DeleteModuleCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public DeleteModuleCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(DeleteModuleCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();

        var lstModule = await _surveyRepo.Module.GetByIds(x => request.Ids.Contains(x.Id));

        if (lstModule == null || lstModule.Count == 0)
            throw new NotFoundException(nameof(Module), request.Ids);

        foreach (var item in lstModule)
            item.Deleted = true;

        await _surveyRepo.Module.UpdateAsync(lstModule);
        await _surveyRepo.SaveAync();
        return new BaseCommandResponse("Xóa thành công!");
    }
}