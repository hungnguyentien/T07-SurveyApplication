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
    

    public class IGuiEmailDtoValitor : AbstractValidator<IGuiEmailDto>
    {
        private readonly IGuiEmailRepository _guiEmailRepository;

        public IGuiEmailDtoValitor(IGuiEmailRepository guiEmailRepository)
        {
            _guiEmailRepository = guiEmailRepository;

            RuleFor(p => p.DiaChiNhan)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.TieuDe)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.NoiDung)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
