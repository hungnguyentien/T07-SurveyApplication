using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Application.Features.GuiEmail.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.GuiEmail.Handlers.Queries;

public class GetGuiEmailDetailRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetGuiEmailDetailRequest, GuiEmailDto>
{
    private readonly IMapper _mapper;

    public GetGuiEmailDetailRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<GuiEmailDto> Handle(GetGuiEmailDetailRequest request, CancellationToken cancellationToken)
    {
        var guiEmailRepository = await _surveyRepo.GuiEmail.GetById(request.Id);
        return _mapper.Map<GuiEmailDto>(guiEmailRepository);
    }
}