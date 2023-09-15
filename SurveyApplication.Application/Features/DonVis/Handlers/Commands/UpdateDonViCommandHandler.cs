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
        if (await _surveyRepo.GuiEmail.Exists(x => request.DonViDto != null && !x.Deleted && x.IdDonVi == request.DonViDto.Id))
            throw new FluentValidation.ValidationException("Đơn vị đã được sử dụng");

        var validator = new UpdateDonViDtoValidator(_surveyRepo.DonVi);
        var validatorResult = await validator.ValidateAsync(request.DonViDto);
        if (validatorResult.IsValid == false) throw new ValidationException(validatorResult);
        var donVi = await _surveyRepo.DonVi.GetById(request.DonViDto?.Id ?? 0);
        _mapper.Map(request.DonViDto, donVi);
        await _surveyRepo.DonVi.Update(donVi);
        await _surveyRepo.SaveAync();
        return Unit.Value;
    }
}