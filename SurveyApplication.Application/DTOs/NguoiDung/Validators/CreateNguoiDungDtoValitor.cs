using FluentValidation;
using SurveyApplication.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.NguoiDung.Validators
{
    
    public class CreateNguoiDungDtoValitor : AbstractValidator<CreateNguoiDungDto>
    {
        private readonly INguoiDungRepository _NguoiDungRepository;

        public CreateNguoiDungDtoValitor(INguoiDungRepository NguoiDungRepository)
        {
            _NguoiDungRepository = NguoiDungRepository;
            Include(new NguoiDungDtoValidator(_NguoiDungRepository));
        }
    }
}
