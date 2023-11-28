using AutoMapper;
using MediatR;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.GuiEmail.Requests.Commands;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.GuiEmail.Handlers.Commands;

public class DeleteGuiEmailCommandHandler : BaseMasterFeatures, IRequestHandler<DeleteGuiEmailCommand>
{
    private readonly IMapper _mapper;

    public DeleteGuiEmailCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteGuiEmailCommand request, CancellationToken cancellationToken)
    {
        var GuiEmailRepository = await _surveyRepo.GuiEmail.GetById(request.Id);
        if (GuiEmailRepository == null) throw new NotFoundException(nameof(GuiEmail), request.Id);
        await _surveyRepo.GuiEmail.Delete(GuiEmailRepository);
        await _surveyRepo.SaveAync();
        return Unit.Value;
    }
}