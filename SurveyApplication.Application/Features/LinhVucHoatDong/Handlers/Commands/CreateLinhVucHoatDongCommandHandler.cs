using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.LinhVucHoatDong.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.LinhVucHoatDong.Handlers.Commands;

public class CreateLinhVucHoatDongCommandHandler : BaseMasterFeatures,
    IRequestHandler<CreateLinhVucHoatDongCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public CreateLinhVucHoatDongCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(CreateLinhVucHoatDongCommand request,
        CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new CreateLinhVucHoatDongDtoValidator(_surveyRepo.LinhVucHoatDong);
        var validatorResult = await validator.ValidateAsync(request.LinhVucHoatDongDto);

        if (validatorResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Tạo mới thất bại";
            response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
            throw new ValidationException(validatorResult);
        }

        var LinhVucHoatDong = _mapper.Map<Domain.LinhVucHoatDong>(request.LinhVucHoatDongDto);
        LinhVucHoatDong = await _surveyRepo.LinhVucHoatDong.Create(LinhVucHoatDong);
        await _surveyRepo.SaveAync();
        response.Success = true;
        response.Message = "Tạo mới thành công";
        response.Id = LinhVucHoatDong.Id;
        return response;
    }
}