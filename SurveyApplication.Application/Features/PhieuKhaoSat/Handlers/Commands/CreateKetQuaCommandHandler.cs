using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.PhieuKhaoSat.Validators;
using SurveyApplication.Application.Enums;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands;
using SurveyApplication.Application.Responses;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Handlers.Commands
{
    public class CreateKetQuaCommandHandler : IRequestHandler<CreateKetQuaCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IKetQuaRepository _ketQuaRepository;
        public CreateKetQuaCommandHandler(IMapper mapper, IKetQuaRepository ketQuaRepository)
        {
            _mapper = mapper;
            _ketQuaRepository = ketQuaRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateKetQuaCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateKetQuaDtoValidator(_ketQuaRepository);
            var validationResult = await validator.ValidateAsync(request.CreateKetQuaDto);
            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Gửi thông tin không thành công";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                 return response;
            }

            var ketQua = _mapper.Map<KetQua>(request.CreateKetQuaDto);
            ketQua = await _ketQuaRepository.Create(ketQua);
            response.Success = true;
            response.Message = "Gửi thông tin thành công";
            response.Id = ketQua.Id.ToString();
            return response;
        }
    }
}
