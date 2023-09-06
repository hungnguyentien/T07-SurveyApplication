using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.BangKhaoSat.Validators
{
    

    public class CreateBangKhaoSatDtoValidator : AbstractValidator<CreateBangKhaoSatDto>
    {
        private readonly IBangKhaoSatRepository _bangKhaoSatRepository;

        public CreateBangKhaoSatDtoValidator(IBangKhaoSatRepository bangKhaoSatRepository)
        {
            _bangKhaoSatRepository = bangKhaoSatRepository;
            Include(new BangKhaoSatDtoValidator(_bangKhaoSatRepository));
        }
    }
}
