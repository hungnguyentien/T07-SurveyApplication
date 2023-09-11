using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.DonVi.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.DonVis.Requests.Commands;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.DonVis.Handlers.Commands;

public class UpdateDonViCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateDonViCommand, Unit>
{
    private readonly IMapper _mapper;

    public UpdateDonViCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateDonViCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateDonViDtoValidator(_surveyRepo.DonVi);
        var validatorResult = await validator.ValidateAsync(request.DonViDto);
        if (validatorResult.IsValid == false) throw new ValidationException(validatorResult);

        var DonVi = await _surveyRepo.DonVi.GetById(request.DonViDto?.Id ?? 0);
        _mapper.Map(request.DonViDto, DonVi);
        await _surveyRepo.DonVi.Update(DonVi);
        await _surveyRepo.SaveAync();
        return Unit.Value;
    }
}