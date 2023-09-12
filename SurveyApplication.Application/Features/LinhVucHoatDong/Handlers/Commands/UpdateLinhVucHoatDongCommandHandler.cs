using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.LinhVucHoatDong.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Commands;
using SurveyApplication.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.LinhVucHoatDong.Handlers.Commands
{
    public class UpdateLinhVucHoatDongCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateLinhVucHoatDongCommand, Unit>
    {
        private readonly IMapper _mapper;

        public UpdateLinhVucHoatDongCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLinhVucHoatDongCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLinhVucHoatDongDtoValidator(_surveyRepo.LinhVucHoatDong);
            var validatorResult = await validator.ValidateAsync(request.LinhVucHoatDongDto);

            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }

            var LinhVucHoatDong = await _surveyRepo.LinhVucHoatDong.GetById(request.LinhVucHoatDongDto?.Id ?? 0);
            _mapper.Map(request.LinhVucHoatDongDto, LinhVucHoatDong);
            await _surveyRepo.LinhVucHoatDong.Update(LinhVucHoatDong);
            await _surveyRepo.SaveAync();
            return Unit.Value;
        }
    }
}
