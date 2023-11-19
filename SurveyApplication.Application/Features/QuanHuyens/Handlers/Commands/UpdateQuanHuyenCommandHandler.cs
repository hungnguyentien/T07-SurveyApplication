using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.QuanHuyen.Validators;
using SurveyApplication.Application.Features.QuanHuyens.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.QuanHuyens.Handlers.Commands;

public class UpdateQuanHuyenCommandHandler : BaseMasterFeatures,
    IRequestHandler<UpdateQuanHuyenCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public UpdateQuanHuyenCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(UpdateQuanHuyenCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new UpdateQuanHuyenDtoValidator(_surveyRepo.QuanHuyen);
        var validatorResult = await validator.ValidateAsync(request.QuanHuyenDto, cancellationToken);
        if (!validatorResult.IsValid)
        {
            response.Success = false;
            response.Message = "Cập nhật thất bại";
            response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
            return response;
        }

        var quanHuyen = await _surveyRepo.QuanHuyen.GetById(request.QuanHuyenDto?.Id ?? 0);
        _mapper.Map(request.QuanHuyenDto, quanHuyen);
        await _surveyRepo.QuanHuyen.Update(quanHuyen);
        await _surveyRepo.SaveAync();

        response.Message = "Cập nhật thành công!";
        response.Id = quanHuyen?.Id ?? 0;
        return response;
    }
}