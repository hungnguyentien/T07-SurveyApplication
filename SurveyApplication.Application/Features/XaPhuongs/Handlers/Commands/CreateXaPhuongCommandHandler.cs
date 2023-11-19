using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.XaPhuong.Validators;
using SurveyApplication.Application.Features.XaPhuongs.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.XaPhuongs.Handlers.Commands;

public class CreateXaPhuongCommandHandler : BaseMasterFeatures,
    IRequestHandler<CreateXaPhuongCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public CreateXaPhuongCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(CreateXaPhuongCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new CreateXaPhuongDtoValidator(_surveyRepo.XaPhuong);
        var validatorResult = await validator.ValidateAsync(request.XaPhuongDto);

        if (validatorResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Tạo mới thất bại";
            response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
            return response;
        }

        var XaPhuong = _mapper.Map<XaPhuong>(request.XaPhuongDto);

        XaPhuong = await _surveyRepo.XaPhuong.Create(XaPhuong);
        await _surveyRepo.SaveAync();

        response.Success = true;
        response.Message = "Tạo mới thành công";
        response.Id = XaPhuong.Id;
        return response;
    }
}