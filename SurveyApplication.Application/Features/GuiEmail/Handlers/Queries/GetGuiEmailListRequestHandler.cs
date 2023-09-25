using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Application.Features.GuiEmail.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.GuiEmail.Handlers.Queries;

public class GetGuiEmailListRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetGuiEmailListRequest, List<GuiEmailDto>>
{
    private readonly IMapper _mapper;

    public GetGuiEmailListRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<List<GuiEmailDto>> Handle(GetGuiEmailListRequest request, CancellationToken cancellationToken)
    {
        var guiEmails = await _surveyRepo.GuiEmail.GetAllListAsync(x => !x.Deleted);
        return _mapper.Map<List<GuiEmailDto>>(guiEmails);
    }
}