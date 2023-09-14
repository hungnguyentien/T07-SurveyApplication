using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.LoaiHinhDonVi.Requests.Commands;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.LoaiHinhDonVi.Handlers.Commands;

public class UpdateLoaiHinhDonViCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateLoaiHinhDonViCommand, Unit>
{
    private readonly IMapper _mapper;

    public UpdateLoaiHinhDonViCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateLoaiHinhDonViCommand request, CancellationToken cancellationToken)
    {
        if (await _surveyRepo.DonVi.Exists(x => request.LoaiHinhDonViDto != null && !x.Deleted && x.IdLoaiHinh == request.LoaiHinhDonViDto.Id))
            throw new FluentValidation.ValidationException("Loại hình đơn vị đã được sử dụng");

        var validator = new UpdateLoaiHinhDonViDtoValidator(_surveyRepo.LoaiHinhDonVi);
        var validatorResult = await validator.ValidateAsync(request.LoaiHinhDonViDto);
        if (validatorResult.IsValid == false) throw new ValidationException(validatorResult);
        var loaiHinhDonVi = await _surveyRepo.LoaiHinhDonVi.GetById(request.LoaiHinhDonViDto?.Id ?? 0);
        _mapper.Map(request.LoaiHinhDonViDto, loaiHinhDonVi);
        await _surveyRepo.LoaiHinhDonVi.Update(loaiHinhDonVi);
        await _surveyRepo.SaveAync();
        return Unit.Value;
    }
}