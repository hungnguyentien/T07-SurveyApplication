using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.DonVi.Validators;
using SurveyApplication.Application.DTOs.DotKhaoSat.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Commands;
using SurveyApplication.Application.Responses;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DotKhaoSats.Handlers.Commands
{
    
    public class CreateDotKhaoSatCommandHandler : IRequestHandler<CreateDotKhaoSatCommand, BaseCommandResponse>
    {
        private readonly IDotKhaoSatRepository _dotKhaoSatRepository;
        private readonly IMapper _mapper;

        public CreateDotKhaoSatCommandHandler(IDotKhaoSatRepository dotKhaoSatRepository, IMapper mapper)
        {
            _dotKhaoSatRepository = dotKhaoSatRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateDotKhaoSatCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateDotKhaoSatDtoValidator(_dotKhaoSatRepository);
            var validatorResult = await validator.ValidateAsync(request.DotKhaoSatDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Tạo mới thất bại";
                response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
                throw new ValidationException(validatorResult);
            }

            var dotKhaoSat = _mapper.Map<DotKhaoSat>(request.DotKhaoSatDto);

            dotKhaoSat = await _dotKhaoSatRepository.Create(dotKhaoSat);

            response.Success = true;
            response.Message = "Tạo mới thành công";
            response.Id = dotKhaoSat.MaDotKhaoSat;
            return response;
        }
    }

}
