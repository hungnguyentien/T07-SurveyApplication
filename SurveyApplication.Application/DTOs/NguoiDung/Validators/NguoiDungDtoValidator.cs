using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.NguoiDung.Validators
{
   
    public class NguoiDungDtoValidator : AbstractValidator<INguoiDungDto>
    {
        private readonly INguoiDungRepository _NguoiDungRepository;
        public NguoiDungDtoValidator(INguoiDungRepository NguoiDungRepository)
        {
            _NguoiDungRepository = NguoiDungRepository;

            RuleFor(p => p.MaNguoiDung)
                .NotNull().NotEmpty().WithMessage("Mã người dùng không được để trống");

            RuleFor(p => p.MaNguoiDung)
                .MustAsync(async (maNguoiDung, token) =>
                {
                    var NguoiDungExists = await _NguoiDungRepository.ExistsByMaNguoiDung(maNguoiDung.ToString());
                    return !NguoiDungExists;
                }).WithMessage("Mã người dùng đã tồn tại!");

            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.PassWord)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.DiaChi)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.HoTen)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.NgaySinh)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.SoDienThoai)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
