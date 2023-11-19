using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.NguoiDaiDien.Validators;
using SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Handlers.Commands;

public class UpdateNguoiDaiDienCommandHandler : BaseMasterFeatures,
    IRequestHandler<UpdateNguoiDaiDienCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public UpdateNguoiDaiDienCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(UpdateNguoiDaiDienCommand request,
        CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new UpdateNguoiDaiDienDtoValidator(_surveyRepo.NguoiDaiDien);
        var validatorResult = await validator.ValidateAsync(request.NguoiDaiDienDto, cancellationToken);
        if (!validatorResult.IsValid)
        {
            response.Success = false;
            response.Message = "Cập nhật thất bại";
            response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
            return response;
        }

        var nguoiDaiDien = await _surveyRepo.NguoiDaiDien.GetById(request.NguoiDaiDienDto?.Id ?? 0);
        _mapper.Map(request.NguoiDaiDienDto, nguoiDaiDien);
        await _surveyRepo.NguoiDaiDien.Update(nguoiDaiDien);
        await _surveyRepo.SaveAync();

        response.Message = "Cập nhật thành công!";
        response.Id = nguoiDaiDien?.Id ?? 0;
        return response;
    }
}