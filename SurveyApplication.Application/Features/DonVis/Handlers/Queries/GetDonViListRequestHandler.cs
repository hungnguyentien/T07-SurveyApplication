using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.Features.DonVis.Requests.Queries;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DonVis.Handlers.Queries
{
   
    public class GetDonViListRequestHandler : IRequestHandler<GetDonViListRequest, List<DonViDto>>
    {
        private readonly IDonViRepository _donViRepository;
        private readonly IMapper _mapper;

        public GetDonViListRequestHandler(IDonViRepository donViRepository, IMapper mapper)
        {
            _donViRepository = donViRepository;
            _mapper = mapper;
        }

        public async Task<List<DonViDto>> Handle(GetDonViListRequest request, CancellationToken cancellationToken)
        {
            var DonVis = await _donViRepository.GetAll();
            return _mapper.Map<List<DonViDto>>(DonVis);
        }
    }
}
