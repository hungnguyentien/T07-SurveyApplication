using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.NguoiDaiDien;
using SurveyApplication.Application.DTOs.NguoiDaiDien.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Handlers.Commands
{
    public class UpdateNguoiDaiDienCommandHandler : IRequestHandler<UpdateNguoiDaiDienCommand, Unit>
    {
        private readonly INguoiDaiDienRepository _nguoiDaiDienRepository;
        private readonly IMapper _mapper;

        public UpdateNguoiDaiDienCommandHandler(INguoiDaiDienRepository nguoiDaiDienRepository, IMapper mapper)
        {
            _nguoiDaiDienRepository = nguoiDaiDienRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateNguoiDaiDienCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateNguoiDaiDienDtoValidator(_nguoiDaiDienRepository);
            var validatorResult = await validator.ValidateAsync(request.NguoiDaiDienDto);

            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }

            var NguoiDaiDien = await _nguoiDaiDienRepository.GetById(request.NguoiDaiDienDto?.Id ?? 0);
            _mapper.Map(request.NguoiDaiDienDto, NguoiDaiDien);
            await _nguoiDaiDienRepository.Update(NguoiDaiDien);
            return Unit.Value;
        }
    }
}
