﻿using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.DotKhaoSat.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.DotKhaoSats.Handlers.Commands
{
    
    public class CreateDotKhaoSatCommandHandler : BaseMasterFeatures, IRequestHandler<CreateDotKhaoSatCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;

        public CreateDotKhaoSatCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateDotKhaoSatCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateDotKhaoSatDtoValidator(_surveyRepo.DotKhaoSat);
            var validatorResult = await validator.ValidateAsync(request.DotKhaoSatDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Tạo mới thất bại";
                response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
                throw new ValidationException(validatorResult);
            }

            var dotKhaoSat = _mapper.Map<DotKhaoSat>(request.DotKhaoSatDto);

            dotKhaoSat = await _surveyRepo.DotKhaoSat.Create(dotKhaoSat);
            await _surveyRepo.SaveAync();

            response.Success = true;
            response.Message = "Tạo mới thành công";
            response.Id = dotKhaoSat.Id;
            return response;
        }
    }
}
