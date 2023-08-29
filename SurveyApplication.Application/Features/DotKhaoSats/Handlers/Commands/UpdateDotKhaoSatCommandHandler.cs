using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.BangKhaoSat.Validators;
using SurveyApplication.Application.DTOs.DotKhaoSat.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DotKhaoSats.Handlers.Commands
{
   
    public class UpdateDotKhaoSatCommandHandler : IRequestHandler<UpdateDotKhaoSatCommand, Unit>
    {
        private readonly IDotKhaoSatRepository _dotKhaoSatRepository;
        private readonly IMapper _mapper;

        public UpdateDotKhaoSatCommandHandler(IDotKhaoSatRepository dotKhaoSatRepository, IMapper mapper)
        {
            _dotKhaoSatRepository = dotKhaoSatRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDotKhaoSatCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateDotKhaoSatDtoValidator(_dotKhaoSatRepository);
            var validatorResult = await validator.ValidateAsync(request.DotKhaoSatDto);

            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }

            var dotKhaoSat = await _dotKhaoSatRepository.GetById(request.DotKhaoSatDto?.Id ?? 0);
            _mapper.Map(request.DotKhaoSatDto, dotKhaoSat);
            await _dotKhaoSatRepository.Update(dotKhaoSat);
            return Unit.Value;
        }
    }
}
