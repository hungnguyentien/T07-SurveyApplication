using AutoMapper;
using MediatR;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands;
using SurveyApplication.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurveyApplication.Application.Responses;
using SurveyApplication.Domain;
using SurveyApplication.Application.Features.Auths.Requests.Commands;

namespace SurveyApplication.Application.Features.Auths.Handlers.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, BaseCommandResponse>
    {
        private readonly IAuthRepository _AuthRepository;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(IAuthRepository AuthRepository, IMapper mapper)
        {
            _AuthRepository = AuthRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var Auth = _mapper.Map<NguoiDung>(request.NguoiDungDto);
            Auth = await _AuthRepository.Create(Auth);
            response.Success = true;
            response.Message = "Tạo mới thành công";
            response.Id = Auth.MaNguoiDung.ToString();
            return response;
        }
    }
}
