using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.DotKhaoSat.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Commands;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.DotKhaoSats.Handlers.Commands;

public class UpdateDotKhaoSatCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateDotKhaoSatCommand, Unit>
{
    private readonly IMapper _mapper;

    public UpdateDotKhaoSatCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateDotKhaoSatCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateDotKhaoSatDtoValidator(_surveyRepo.DotKhaoSat);
        var validatorResult = await validator.ValidateAsync(request.DotKhaoSatDto, cancellationToken);
        if (validatorResult.IsValid == false) throw new ValidationException(validatorResult);
        if(await _surveyRepo.BangKhaoSat.Exists(x => x.IdDotKhaoSat == request.DotKhaoSatDto.Id)) throw new FluentValidation.ValidationException("Đợt khảo sát đã được sử dụng");
        var dotKhaoSat = await _surveyRepo.DotKhaoSat.GetById(request.DotKhaoSatDto?.Id ?? 0);
        _mapper.Map(request.DotKhaoSatDto, dotKhaoSat);
        await _surveyRepo.DotKhaoSat.Update(dotKhaoSat);
        await _surveyRepo.SaveAync();
        return Unit.Value;
    }
}