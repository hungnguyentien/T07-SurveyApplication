using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.Responses;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Handlers.Commands
{
    public class CreateLoaiHinhDonViCommandHandler : IRequestHandler<CreateLoaiHinhDonViCommand, BaseCommandResponse>
    {
        private readonly ILoaiHinhDonViRepository _loaiHinhDonViRepository;
        private readonly IMapper _mapper;

        public CreateLoaiHinhDonViCommandHandler(ILoaiHinhDonViRepository loaiHinhDonViRepository, IMapper mapper)
        {
            _loaiHinhDonViRepository = loaiHinhDonViRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLoaiHinhDonViCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var loaiHinhDonVi = _mapper.Map<LoaiHinhDonVi>(request.LoaiHinhDonViDto);
            loaiHinhDonVi = await _loaiHinhDonViRepository.Create(loaiHinhDonVi);
            response.Success = true;
            response.Message = "Tạo mới thành công";
            response.Id = loaiHinhDonVi.MaLoaiHinh;
            return response;
        }
    }
}
