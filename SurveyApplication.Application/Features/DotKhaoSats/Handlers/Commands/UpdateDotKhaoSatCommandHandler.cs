using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.DotKhaoSat.Validators;
using SurveyApplication.Application.DTOs.DotKhaoSat.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.DotKhaoSats.Handlers.Commands;

public class UpdateDotKhaoSatCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateDotKhaoSatCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public UpdateDotKhaoSatCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(UpdateDotKhaoSatCommand request, CancellationToken cancellationToken)
    {
        if(await _surveyRepo.BangKhaoSat.Exists(x => x.IdDotKhaoSat == request.DotKhaoSatDto.Id)) throw new FluentValidation.ValidationException("Đợt khảo sát đã được sử dụng");
        
        var response = new BaseCommandResponse();
        var validator = new UpdateDotKhaoSatDtoValidator(_surveyRepo.DotKhaoSat);
        var validatorResult = await validator.ValidateAsync(request.DotKhaoSatDto, cancellationToken);
        if (!validatorResult.IsValid)
        {
            response.Success = false;
            response.Message = "Cập nhật thất bại";
            response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
            return response;
        }

        var dotKhaoSat = await _surveyRepo.DotKhaoSat.GetById(request.DotKhaoSatDto?.Id ?? 0);
        _mapper.Map(request.DotKhaoSatDto, dotKhaoSat);
        await _surveyRepo.DotKhaoSat.Update(dotKhaoSat);
        await _surveyRepo.SaveAync();

        response.Message = "Cập nhật thành công!";
        response.Id = dotKhaoSat?.Id ?? 0;
        return response;
    }
}