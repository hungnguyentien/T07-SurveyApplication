using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.GuiEmail.Validators
{
    
    public class CreateGuiEmailDtoValidator : AbstractValidator<CreateGuiEmailDto>
    {
        private readonly IGuiEmailRepository _guiEmailRepository;

        public CreateGuiEmailDtoValidator(IGuiEmailRepository guiEmailRepository)
        {
            _guiEmailRepository = guiEmailRepository;
            Include(new GuiEmailDtoValidator(_guiEmailRepository));
        }
    }
}
