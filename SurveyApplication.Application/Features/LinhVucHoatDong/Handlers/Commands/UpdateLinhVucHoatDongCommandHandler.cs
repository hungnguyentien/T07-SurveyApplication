using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.LinhVucHoatDong.Validators;
using SurveyApplication.Application.DTOs.LinhVucHoatDong.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.LinhVucHoatDong.Handlers.Commands
{
    public class UpdateLinhVucHoatDongCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateLinhVucHoatDongCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;

        public UpdateLinhVucHoatDongCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateLinhVucHoatDongCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateLinhVucHoatDongDtoValidator(_surveyRepo.LinhVucHoatDong);
            var validatorResult = await validator.ValidateAsync(request.LinhVucHoatDongDto, cancellationToken);
            if (!validatorResult.IsValid)
            {
                response.Success = false;
                response.Message = "Cập nhật thất bại";
                response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
                return response;
            }

            var linhVucHoatDong = await _surveyRepo.LinhVucHoatDong.GetById(request.LinhVucHoatDongDto?.Id ?? 0);
            _mapper.Map(request.LinhVucHoatDongDto, linhVucHoatDong);
            await _surveyRepo.LinhVucHoatDong.Update(linhVucHoatDong);
            await _surveyRepo.SaveAync();

            response.Message = "Cập nhật thành công!";
            response.Id = linhVucHoatDong?.Id ?? 0;
            return response;
        }
    }
}
