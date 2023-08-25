using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.GuiEmail.Validators
{
   
    public class GuiEmailDtoValidator : AbstractValidator<IGuiEmailDto>
    {
        private readonly IGuiEmailRepository _guiEmailRepository;
        public GuiEmailDtoValidator(IGuiEmailRepository guiEmailRepository)
        {
            _guiEmailRepository = guiEmailRepository;
            RuleFor(p => p.MaGuiEmail)
                .NotNull().NotEmpty().WithMessage("Mã gửi email không được để trống");
            RuleFor(p => p.MaGuiEmail)
                .MustAsync(async (maGuiEmail, token) =>
                {
                    var guiEmailExists = await _guiEmailRepository.ExistsByMaGuiEmail(maGuiEmail); ;
                    return !guiEmailExists;
                }).WithMessage("Mã bảng khảo sát đã tồn tại!");
        }
    }
}
