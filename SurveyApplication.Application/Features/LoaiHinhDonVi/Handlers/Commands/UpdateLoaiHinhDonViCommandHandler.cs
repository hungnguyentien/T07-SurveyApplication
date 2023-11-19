using AutoMapper;
using FluentValidation;
using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi.Validators;
using SurveyApplication.Application.Features.LoaiHinhDonVi.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.LoaiHinhDonVi.Handlers.Commands;

public class UpdateLoaiHinhDonViCommandHandler : BaseMasterFeatures,
    IRequestHandler<UpdateLoaiHinhDonViCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public UpdateLoaiHinhDonViCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(UpdateLoaiHinhDonViCommand request,
        CancellationToken cancellationToken)
    {
        if (await _surveyRepo.DonVi.Exists(x =>
                request.LoaiHinhDonViDto != null && !x.Deleted && x.IdLoaiHinh == request.LoaiHinhDonViDto.Id))
            throw new ValidationException("Loại hình đơn vị đã được sử dụng");

        var response = new BaseCommandResponse();
        var validator = new UpdateLoaiHinhDonViDtoValidator(_surveyRepo.LoaiHinhDonVi);
        var validatorResult = await validator.ValidateAsync(request.LoaiHinhDonViDto, cancellationToken);
        if (!validatorResult.IsValid)
        {
            response.Success = false;
            response.Message = "Cập nhật thất bại";
            response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
            return response;
        }

        var loaiHinhDonVi = await _surveyRepo.LoaiHinhDonVi.GetById(request.LoaiHinhDonViDto?.Id ?? 0);
        _mapper.Map(request.LoaiHinhDonViDto, loaiHinhDonVi);
        await _surveyRepo.LoaiHinhDonVi.Update(loaiHinhDonVi);
        await _surveyRepo.SaveAync();

        response.Message = "Cập nhật thành công!";
        response.Id = loaiHinhDonVi?.Id ?? 0;
        return response;
    }
}