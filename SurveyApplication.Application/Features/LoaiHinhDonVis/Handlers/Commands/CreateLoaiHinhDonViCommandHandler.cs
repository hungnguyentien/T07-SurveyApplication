using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Handlers.Commands;

public class CreateLoaiHinhDonViCommandHandler : BaseMasterFeatures,
    IRequestHandler<CreateLoaiHinhDonViCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public CreateLoaiHinhDonViCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(CreateLoaiHinhDonViCommand request,
        CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new CreateLoaiHinhDonViDtoValidator(_surveyRepo.LoaiHinhDonVi);
        var validatorResult = await validator.ValidateAsync(request.LoaiHinhDonViDto);

        if (validatorResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Tạo mới thất bại";
            response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
            throw new ValidationException(validatorResult);
        }

        var LoaiHinhDonVi = _mapper.Map<LoaiHinhDonVi>(request.LoaiHinhDonViDto);

        LoaiHinhDonVi = await _surveyRepo.LoaiHinhDonVi.Create(LoaiHinhDonVi);
        await _surveyRepo.SaveAync();

        response.Success = true;
        response.Message = "Tạo mới thành công";
        response.Id = LoaiHinhDonVi.Id;
        return response;
    }
}