using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.TinhTp.Validators;
using SurveyApplication.Application.Features.TinhTps.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.TinhTps.Handlers.Commands;

public class CreateTinhTpCommandHandler : BaseMasterFeatures, IRequestHandler<CreateTinhTpCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public CreateTinhTpCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(CreateTinhTpCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new CreateTinhTpDtoValidator(_surveyRepo.TinhTp);
        var validatorResult = await validator.ValidateAsync(request.TinhTpDto);

        if (validatorResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Tạo mới thất bại";
            response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
            return response;
        }

        var TinhTp = _mapper.Map<TinhTp>(request.TinhTpDto);

        TinhTp = await _surveyRepo.TinhTp.Create(TinhTp);
        await _surveyRepo.SaveAync();

        response.Success = true;
        response.Message = "Tạo mới thành công";
        response.Id = TinhTp.Id;
        return response;
    }
}