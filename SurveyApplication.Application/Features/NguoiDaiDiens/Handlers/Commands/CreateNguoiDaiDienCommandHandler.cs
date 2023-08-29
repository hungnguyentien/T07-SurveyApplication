using AutoMapper;
using MediatR;
using SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Commands;
using SurveyApplication.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurveyApplication.Application.Responses;
using SurveyApplication.Domain;
using SurveyApplication.Application.DTOs.NguoiDaiDien.Validators;
using SurveyApplication.Application.Exceptions;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Handlers.Commands
{
    public class CreateNguoiDaiDienCommandHandler : IRequestHandler<CreateNguoiDaiDienCommand, BaseCommandResponse>
    {
        private readonly INguoiDaiDienRepository _nguoiDaiDienRepository;
        private readonly IMapper _mapper;

        public CreateNguoiDaiDienCommandHandler(INguoiDaiDienRepository nguoiDaiDienRepository, IMapper mapper)
        {
            _nguoiDaiDienRepository = nguoiDaiDienRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateNguoiDaiDienCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateNguoiDaiDienDtoValidator(_nguoiDaiDienRepository);
            var validatorResult = await validator.ValidateAsync(request.NguoiDaiDienDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Tạo mới thất bại";
                response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
                throw new ValidationException(validatorResult);
            }

            var NguoiDaiDien = _mapper.Map<NguoiDaiDien>(request.NguoiDaiDienDto);

            NguoiDaiDien = await _nguoiDaiDienRepository.Create(NguoiDaiDien);

            response.Success = true;
            response.Message = "Tạo mới thành công";
            response.Id = NguoiDaiDien.MaNguoiDaiDien.ToString();
            return response;
        }
    }
}
