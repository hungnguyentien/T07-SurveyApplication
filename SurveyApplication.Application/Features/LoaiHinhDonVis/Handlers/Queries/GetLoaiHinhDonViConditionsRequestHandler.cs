using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Handlers.Queries
{
   
    public class GetLoaiHinhDonViConditionsRequestHandler : IRequestHandler<GetLoaiHinhDonViConditionsRequest, List<LoaiHinhDonViDto>>
    {
        private readonly ILoaiHinhDonViRepository _LoaiHinhDonViRepository;
        private readonly IMapper _mapper;
        public GetLoaiHinhDonViConditionsRequestHandler(ILoaiHinhDonViRepository LoaiHinhDonViRepository, IMapper mapper)
        {
            _LoaiHinhDonViRepository = LoaiHinhDonViRepository;
            _mapper = mapper;
        }

        public async Task<List<LoaiHinhDonViDto>> Handle(GetLoaiHinhDonViConditionsRequest request, CancellationToken cancellationToken)
        {
            var LoaiHinhDonVis = await _LoaiHinhDonViRepository.GetByConditions(request.PageIndex, request.PageSize, x => string.IsNullOrEmpty(request.Keyword) || !string.IsNullOrEmpty(x.TenLoaiHinh) && x.TenLoaiHinh.Contains(request.Keyword));
            return _mapper.Map<List<LoaiHinhDonViDto>>(LoaiHinhDonVis);
        }
    }

}
