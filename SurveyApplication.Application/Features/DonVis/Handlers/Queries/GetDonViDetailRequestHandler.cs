using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.Features.DonVis.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DonVis.Handlers.Queries
{
    public class GetDonViDetailRequestHandler : IRequestHandler<GetDonViDetailRequest, DonViDto>
    {
        private readonly IDonViRepository _donViRepository;
        private readonly IMapper _mapper;

        public GetDonViDetailRequestHandler(IDonViRepository donViRepository, IMapper mapper)
        {
            _donViRepository = donViRepository;
            _mapper = mapper;
        }

        public async Task<DonViDto> Handle(GetDonViDetailRequest request, CancellationToken cancellationToken)
        {
            var DonViRepository = await _donViRepository.GetById(request.Id);
            return _mapper.Map<DonViDto>(DonViRepository);
        }
    }
    
}
