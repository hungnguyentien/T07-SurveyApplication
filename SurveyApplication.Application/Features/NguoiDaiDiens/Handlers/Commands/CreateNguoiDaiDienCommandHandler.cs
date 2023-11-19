using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.NguoiDaiDien.Validators;
using SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Handlers.Commands;

public class CreateNguoiDaiDienCommandHandler : BaseMasterFeatures,
    IRequestHandler<CreateNguoiDaiDienCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public CreateNguoiDaiDienCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(CreateNguoiDaiDienCommand request,
        CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new CreateNguoiDaiDienDtoValidator(_surveyRepo.NguoiDaiDien);
        var validatorResult = await validator.ValidateAsync(request.NguoiDaiDienDto);

        if (validatorResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Tạo mới thất bại";
            response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
            return response;
        }

        var NguoiDaiDien = _mapper.Map<NguoiDaiDien>(request.NguoiDaiDienDto);
        NguoiDaiDien.MaNguoiDaiDien = Guid.NewGuid().ToString();
        NguoiDaiDien = await _surveyRepo.NguoiDaiDien.Create(NguoiDaiDien);
        await _surveyRepo.SaveAync();
        response.Success = true;
        response.Message = "Tạo mới thành công";
        response.Id = NguoiDaiDien.Id;
        return response;
    }
}