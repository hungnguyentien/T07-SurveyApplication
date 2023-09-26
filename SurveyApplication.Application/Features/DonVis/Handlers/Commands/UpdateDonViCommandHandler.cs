using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.DonVi.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.DonVis.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.DonVis.Handlers.Commands;

public class UpdateDonViCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateDonViCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public UpdateDonViCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(UpdateDonViCommand request, CancellationToken cancellationToken)
    {
        if (await _surveyRepo.GuiEmail.Exists(x => request.DonViDto != null && !x.Deleted && x.IdDonVi == request.DonViDto.Id))
            throw new FluentValidation.ValidationException("Đơn vị đã được sử dụng");

        var response = new BaseCommandResponse();
        var validator = new UpdateDonViDtoValidator(_surveyRepo.DonVi);
        var validatorResult = await validator.ValidateAsync(request.DonViDto, cancellationToken);
        if (!validatorResult.IsValid)
        {
            response.Success = false;
            response.Message = "Cập nhật thất bại";
            response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
            return response;
        }

        var donVi = await _surveyRepo.DonVi.GetById(request.DonViDto?.Id ?? 0);
        _mapper.Map(request.DonViDto, donVi);
        await _surveyRepo.DonVi.Update(donVi);
        await _surveyRepo.SaveAync();

        response.Message = "Cập nhật thành công!";
        response.Id = donVi?.Id ?? 0;
        return response;
    }
}