using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.GuiEmail.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.GuiEmail.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.GuiEmail.Handlers.Commands;

public class CreateGuiEmailCommandHandler : BaseMasterFeatures,
    IRequestHandler<CreateGuiEmailCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public CreateGuiEmailCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(CreateGuiEmailCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new CreateGuiEmailDtoValidator(_surveyRepo.GuiEmail);
        if (request.GuiEmailDto != null)
        {
            var validatorResult = await validator.ValidateAsync(request.GuiEmailDto, cancellationToken);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Tạo mới thất bại";
                response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
                throw new ValidationException(validatorResult);
            }
        }

        var guiEmail = _mapper.Map<Domain.GuiEmail>(request.GuiEmailDto);
        guiEmail = await _surveyRepo.GuiEmail.Create(guiEmail);
        await _surveyRepo.SaveAync();
        response.Success = true;
        response.Message = "Tạo mới thành công";
        response.Id = guiEmail.Id;
        return response;
    }
}