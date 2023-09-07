using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.GuiEmail.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.GuiEmail.Requests.Commands;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.GuiEmail.Handlers.Commands
{
   
    public class UpdateGuiEmailCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateGuiEmailCommand, Unit>
    {
        private readonly IMapper _mapper;

        public UpdateGuiEmailCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateGuiEmailCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateGuiEmailDtoValidator(_surveyRepo.GuiEmail);
            var validatorResult = await validator.ValidateAsync(request.GuiEmailDto);

            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }

            var guiEmail = await _surveyRepo.GuiEmail.GetById(request.GuiEmailDto?.Id ?? 0);
            _mapper.Map(request.GuiEmailDto, guiEmail);
            await _surveyRepo.GuiEmail.Update(guiEmail);
            await _surveyRepo.SaveAync();
            return Unit.Value;
        }
    }
}
