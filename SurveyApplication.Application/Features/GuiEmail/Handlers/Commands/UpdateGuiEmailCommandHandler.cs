using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.GuiEmail.Validators;
using SurveyApplication.Application.Features.GuiEmail.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.GuiEmail.Handlers.Commands;

public class UpdateGuiEmailCommandHandler : BaseMasterFeatures,
    IRequestHandler<UpdateGuiEmailCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public UpdateGuiEmailCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(UpdateGuiEmailCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new UpdateGuiEmailDtoValidator(_surveyRepo.GuiEmail);
        var validatorResult = await validator.ValidateAsync(request.GuiEmailDto, cancellationToken);
        if (!validatorResult.IsValid)
        {
            response.Success = false;
            response.Message = "Cập nhật thất bại";
            response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
            return response;
        }

        var guiEmail = await _surveyRepo.GuiEmail.GetById(request.GuiEmailDto?.Id ?? 0);
        _mapper.Map(request.GuiEmailDto, guiEmail);
        await _surveyRepo.GuiEmail.Update(guiEmail);
        await _surveyRepo.SaveAync();

        response.Message = "Cập nhật thành công!";
        response.Id = guiEmail?.Id ?? 0;
        return response;
    }
}