using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.GuiEmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.GuiEmail.Validators
{
    
    public class UpdateGuiEmailDtoValidator : AbstractValidator<UpdateGuiEmailDto>
    {
        private readonly IGuiEmailRepository _guiEmailRepository;

        public UpdateGuiEmailDtoValidator(IGuiEmailRepository guiEmailRepository)
        {
            _guiEmailRepository = guiEmailRepository;
            Include(new IGuiEmailDtoValidator(_guiEmailRepository));

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
