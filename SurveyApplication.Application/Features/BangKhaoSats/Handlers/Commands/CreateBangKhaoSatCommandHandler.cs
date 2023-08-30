using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurveyApplication.Application.Responses;
using SurveyApplication.Domain;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands;
using SurveyApplication.Application.DTOs.BangKhaoSat.Validators;
using SurveyApplication.Application.Exceptions;

namespace SurveyApplication.Application.Features.BangKhaoSats.Handlers.Commands
{
    public class CreateBangKhaoSatCommandHandler : IRequestHandler<CreateBangKhaoSatCommand, BaseCommandResponse>
    {
        private readonly IBangKhaoSatRepository _bangKhaoSatRepository;
        private readonly IMapper _mapper;

        public CreateBangKhaoSatCommandHandler(IBangKhaoSatRepository bangKhaoSatRepository, IMapper mapper)
        {
            _bangKhaoSatRepository = bangKhaoSatRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateBangKhaoSatCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateBangKhaoSatDtoValidator(_bangKhaoSatRepository);
            var validatorResult = await validator.ValidateAsync(request.BangKhaoSatDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Tạo mới thất bại";
                response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
                throw new ValidationException(validatorResult);
            }

            var bangKhaoSat = _mapper.Map<BangKhaoSat>(request.BangKhaoSatDto);

            bangKhaoSat = await _bangKhaoSatRepository.Create(bangKhaoSat);

            response.Success = true;
            response.Message = "Tạo mới thành công";
            response.Id = bangKhaoSat.Id;
            return response;
        }
    }
}
