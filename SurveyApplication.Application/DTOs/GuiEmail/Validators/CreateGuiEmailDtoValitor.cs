using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.GuiEmail.Validators
{
    
    public class CreateGuiEmailDtoValitor : AbstractValidator<CreateGuiEmailDto>
    {
        private readonly IGuiEmailRepository _guiEmailRepository;

        public CreateGuiEmailDtoValitor(IGuiEmailRepository guiEmailRepository)
        {
            _guiEmailRepository = guiEmailRepository;
            Include(new GuiEmailDtoValidator(_guiEmailRepository));
        }
    }
}
