using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.DTOs.BangKhaoSat.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.BangKhaoSats.Handlers.Commands;

public class UpdateBangKhaoSatCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateBangKhaoSatCommand, Unit>
{
    private readonly IMapper _mapper;

    public UpdateBangKhaoSatCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateBangKhaoSatCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateBangKhaoSatDtoValidator(_surveyRepo.BangKhaoSat);
        var validatorResult =
            await validator.ValidateAsync(request.BangKhaoSatDto ?? new UpdateBangKhaoSatDto(), cancellationToken);
        if (validatorResult.IsValid == false)
            throw new ValidationException(validatorResult);

        var bangKhaoSat = await _surveyRepo.BangKhaoSat.GetById(request.BangKhaoSatDto?.Id ?? 0);
        _mapper.Map(request.BangKhaoSatDto, bangKhaoSat);
        await _surveyRepo.BangKhaoSat.UpdateAsync(bangKhaoSat);
        await _surveyRepo.SaveAync();
        if (request.BangKhaoSatDto?.BangKhaoSatCauHoi == null) return Unit.Value;
        var lstBangKhaoSatCauHoi = _mapper.Map<List<BangKhaoSatCauHoi>>(request.BangKhaoSatDto.BangKhaoSatCauHoi);
        if (lstBangKhaoSatCauHoi == null) return Unit.Value;
        var lstRemove = await _surveyRepo.BangKhaoSatCauHoi.GetAllListAsync(x => x.IdBangKhaoSat == bangKhaoSat.Id);
        await _surveyRepo.BangKhaoSatCauHoi.DeleteAsync(lstRemove);
        lstBangKhaoSatCauHoi.ForEach(x =>
        {
            x.IdBangKhaoSat = bangKhaoSat.Id;
            x.Priority = lstBangKhaoSatCauHoi.FindIndex(iBks => iBks == x);
        });
        await _surveyRepo.BangKhaoSatCauHoi.Creates(lstBangKhaoSatCauHoi);
        await _surveyRepo.SaveAync();
        return Unit.Value;
    }
}