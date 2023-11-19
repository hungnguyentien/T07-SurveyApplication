using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.XaPhuong.Validators;
using SurveyApplication.Application.Features.XaPhuongs.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.XaPhuongs.Handlers.Commands;

public class UpdateXaPhuongCommandHandler : BaseMasterFeatures,
    IRequestHandler<UpdateXaPhuongCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public UpdateXaPhuongCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(UpdateXaPhuongCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new UpdateXaPhuongDtoValidator(_surveyRepo.XaPhuong);
        var validatorResult = await validator.ValidateAsync(request.XaPhuongDto, cancellationToken);
        if (!validatorResult.IsValid)
        {
            response.Success = false;
            response.Message = "Cập nhật thất bại";
            response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
            return response;
        }

        var xaPhuong = await _surveyRepo.XaPhuong.GetById(request.XaPhuongDto?.Id ?? 0);
        _mapper.Map(request.XaPhuongDto, xaPhuong);
        await _surveyRepo.XaPhuong.Update(xaPhuong);
        await _surveyRepo.SaveAync();

        response.Message = "Cập nhật thành công!";
        response.Id = xaPhuong?.Id ?? 0;
        return response;
    }
}