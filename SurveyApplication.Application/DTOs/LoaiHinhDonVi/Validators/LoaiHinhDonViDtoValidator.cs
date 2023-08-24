using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.LoaiHinhDonVi.Validators
{
    public class LoaiHinhDonViDtoValidator : AbstractValidator<ILoaiHinhDonViDto>
    {
        private readonly ILoaiHinhDonViRepository _loaiHinhDonViRepository;
        public LoaiHinhDonViDtoValidator(ILoaiHinhDonViRepository loaiHinhDonViRepository)
        {
            _loaiHinhDonViRepository = loaiHinhDonViRepository;
            RuleFor(p => p.MaLoaiHinh)
                .NotNull().NotEmpty().WithMessage("Mã loại hình không được để trống");
            RuleFor(p => p.MaLoaiHinh)
                .MustAsync(async (maLoaiHinh, token) =>
                {
                    var loaiHinhDonViExists = await _loaiHinhDonViRepository.ExistsByMaLoaiHinh(maLoaiHinh);
                    return !loaiHinhDonViExists;
                }).WithMessage("Mã loại hình đã tồn tại!");
        }
    }
}
