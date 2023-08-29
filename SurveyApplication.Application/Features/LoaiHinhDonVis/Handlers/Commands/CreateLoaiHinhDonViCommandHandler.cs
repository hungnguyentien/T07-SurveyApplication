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
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Handlers.Commands
{
    public class CreateLoaiHinhDonViCommandHandler : IRequestHandler<CreateLoaiHinhDonViCommand, BaseCommandResponse>
    {
        private readonly ILoaiHinhDonViRepository _LoaiHinhDonViRepository;
        private readonly IMapper _mapper;

        public CreateLoaiHinhDonViCommandHandler(ILoaiHinhDonViRepository LoaiHinhDonViRepository, IMapper mapper)
        {
            _LoaiHinhDonViRepository = LoaiHinhDonViRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLoaiHinhDonViCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var LoaiHinhDonVi = _mapper.Map<LoaiHinhDonVi>(request.LoaiHinhDonViDto);
            LoaiHinhDonVi = await _LoaiHinhDonViRepository.Create(LoaiHinhDonVi);
            response.Success = true;
            response.Message = "Tạo mới thành công";
            response.Id = LoaiHinhDonVi.MaLoaiHinh;
            return response;
        }
    }
}
