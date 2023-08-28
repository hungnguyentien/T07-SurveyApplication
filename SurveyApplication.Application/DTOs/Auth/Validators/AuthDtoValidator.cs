using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.Auth.Validators
{
   
    public class AuthDtoValidator : AbstractValidator<IAuthDto>
    {
        private readonly IAuthRepository _AuthRepository;
        public AuthDtoValidator(IAuthRepository AuthRepository)
        {
            _AuthRepository = AuthRepository;

            RuleFor(p => p.DiaChi)
                .NotNull().NotEmpty().WithMessage("Mã gửi email không được để trống");

            RuleFor(p => p.MaNguoiDung)
                .MustAsync(async (maAuth, token) =>
                {
                    var AuthExists = await _AuthRepository.ExistsByMaAuth(maAuth.ToString()); ;
                    return !AuthExists;
                }).WithMessage("Mã bảng khảo sát đã tồn tại!");
        }
    }
}
