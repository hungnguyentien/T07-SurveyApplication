using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands;
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
    
    public class CreateGuiEmailCommandHandler : IRequestHandler<CreatGuiEmailCommand, BaseCommandResponse>
    {
        private readonly IDotKhaoSatRepository _dotKhaoSatRepository;
        private readonly IMapper _mapper;

        public CreateGuiEmailCommandHandler(IDotKhaoSatRepository dotKhaoSatRepository, IMapper mapper)
        {
            _dotKhaoSatRepository = dotKhaoSatRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreatGuiEmailCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var dotKhaoSat = _mapper.Map<DotKhaoSat>(request.DotKhaoSatDto);
            dotKhaoSat = await _dotKhaoSatRepository.Create(dotKhaoSat);
            response.Success = true;
            response.Message = "Tạo mới thành công";
            response.Id = dotKhaoSat.MaDotKhaoSat;
            return response;
        }
    }

}
