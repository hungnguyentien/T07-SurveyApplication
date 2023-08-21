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
    public class GetLoaiHinhDonViListRequestHandler : IRequestHandler<GetLoaiHinhDonViListRequest, List<LoaiHinhDonViDto>>
    {
        private readonly ILoaiHinhDonViRepository _loaiHinhDonViRepository;
        private readonly IMapper _mapper;

        public GetLoaiHinhDonViListRequestHandler(ILoaiHinhDonViRepository loaiHinhDonViRepository, IMapper mapper)
        {
            _loaiHinhDonViRepository = loaiHinhDonViRepository;
            _mapper = mapper;
        }

        public async Task<List<LoaiHinhDonViDto>> Handle(GetLoaiHinhDonViListRequest request, CancellationToken cancellationToken)
        {
            var loaiHinhDonVis = await _loaiHinhDonViRepository.GetAll();
            return _mapper.Map<List<LoaiHinhDonViDto>>(loaiHinhDonVis);
        }
    }
}
