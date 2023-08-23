using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.Features.Ba.Requests.Queries;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.BangKhaoSats.Handlers.Queries
{
    public class GetBangKhaoSatDetailRequestHandler : IRequestHandler<GetBangKhaoSatDetailRequest, BangKhaoSatDto>
    {
        private readonly IBangKhaoSatRepository _bangKhaoSatRepository;
        private readonly IMapper _mapper;

        public GetBangKhaoSatDetailRequestHandler(IBangKhaoSatRepository bangKhaoSatRepository, IMapper mapper)
        {
            _bangKhaoSatRepository = bangKhaoSatRepository;
            _mapper = mapper;
        }

        public async Task<BangKhaoSatDto> Handle(GetBangKhaoSatDetailRequest request, CancellationToken cancellationToken)
        {
            var loaiHinhDonVis = await _loaiHinhDonViRepository.GetById(request.MaLoaiHinh);
            return _mapper.Map<LoaiHinhDonViDto>(loaiHinhDonVis);
        }
    }
    
}
