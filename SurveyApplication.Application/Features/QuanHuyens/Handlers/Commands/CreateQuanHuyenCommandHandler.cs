using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.QuanHuyen.Validators;
using SurveyApplication.Application.Features.QuanHuyens.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.QuanHuyens.Handlers.Commands;

public class CreateQuanHuyenCommandHandler : BaseMasterFeatures,
    IRequestHandler<CreateQuanHuyenCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public CreateQuanHuyenCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(CreateQuanHuyenCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new CreateQuanHuyenDtoValidator(_surveyRepo.QuanHuyen);
        var validatorResult = await validator.ValidateAsync(request.QuanHuyenDto);

        if (validatorResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Tạo mới thất bại";
            response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
            return response;
        }

        var QuanHuyen = _mapper.Map<QuanHuyen>(request.QuanHuyenDto);

        QuanHuyen = await _surveyRepo.QuanHuyen.Create(QuanHuyen);
        await _surveyRepo.SaveAync();

        response.Success = true;
        response.Message = "Tạo mới thành công";
        response.Id = QuanHuyen.Id;
        return response;
    }
}