using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.DTOs.DonVi.Validators;
using SurveyApplication.Application.Features.CauHoi.Requests.Commands;
using SurveyApplication.Application.Features.DonVis.Requests.Commands;
using SurveyApplication.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.CauHoi.Handlers.Commands
{
    public class UpdateCauHoiCommandHandler : IRequestHandler<UpdateCauHoiCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICauHoiRepository _cauHoiRepository;
        public UpdateCauHoiCommandHandler(IMapper mapper, ICauHoiRepository cauHoiRepository)
        {
            _mapper = mapper;
            _cauHoiRepository = cauHoiRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateCauHoiCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            //var validator = new UpdateDonViDtoValidator(_donViRepository);
            //var validatorResult = await validator.ValidateAsync(request.DonViDto);

            //if (validatorResult.IsValid == false)
            //{
            //    throw new ValidationException(validatorResult);
            //}

            var cauHoi = await _cauHoiRepository.GetById(request.CauHoiDto.Id);
            _mapper.Map(request.CauHoiDto, cauHoi);
            await _cauHoiRepository.Update(cauHoi);
            response.Message = "Cập nhật thành công!";
            response.Id = cauHoi.Id;
            return response;
        }
    }
}
