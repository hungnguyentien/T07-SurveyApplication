using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries;
using SurveyApplication.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Handlers.Queries
{
    public class GetLoaiHinhDonViDetailRequestHandler : IRequestHandler<GetLoaiHinhDonViDetailRequest, LoaiHinhDonViDto>
    {
        private readonly ILoaiHinhDonViRepository _loaiHinhDonViRepository;
        private readonly IMapper _mapper;

        public GetLoaiHinhDonViDetailRequestHandler(ILoaiHinhDonViRepository loaiHinhDonViRepository, IMapper mapper)
        {
            _loaiHinhDonViRepository = loaiHinhDonViRepository;
            _mapper = mapper;
        }

        public async Task<LoaiHinhDonViDto> Handle(GetLoaiHinhDonViDetailRequest request, CancellationToken cancellationToken)
        {
            var loaiHinhDonVis = await _loaiHinhDonViRepository.GetById(request.Maloaihinh);
            return _mapper.Map<LoaiHinhDonViDto>(loaiHinhDonVis);
        }
    }
}
