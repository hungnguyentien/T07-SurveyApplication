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
                .NotNull().NotEmpty().WithMessage("Mã gửi email không được để trống");
            RuleFor(p => p.MaNguoiDung)
                .MustAsync(async (maNguoiDung, token) =>
                {
                    var NguoiDungExists = await _NguoiDungRepository.ExistsByMaNguoiDung(maNguoiDung.ToString());
                    return !NguoiDungExists;
                }).WithMessage("Mã bảng khảo sát đã tồn tại!");
        }
    }
}
