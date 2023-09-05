using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.Features.DonVis.Requests.Queries;
using SurveyApplication.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DonVis.Handlers.Queries
{
   
    public class GetDonViConditionsRequestHandler : IRequestHandler<GetDonViConditionsRequest, PageCommandResponse<DonViDto>>
    {
        private readonly IDonViRepository _donViRepository;
        private readonly IMapper _mapper;
        public GetDonViConditionsRequestHandler(IDonViRepository donViRepository, IMapper mapper)
        {
            _donViRepository = donViRepository;
            _mapper = mapper;
        }

        public async Task<PageCommandResponse<DonViDto>> Handle(GetDonViConditionsRequest request, CancellationToken cancellationToken)
        {
            var DonVis = await _donViRepository.GetByCondition(request.PageIndex, request.PageSize, request.Keyword, x => string.IsNullOrEmpty(request.Keyword) || !string.IsNullOrEmpty(x.TenDonVi) && x.TenDonVi.Contains(request.Keyword), x => x.Created);
            return _mapper.Map<PageCommandResponse<DonViDto>>(DonVis);
        }
    }

}
