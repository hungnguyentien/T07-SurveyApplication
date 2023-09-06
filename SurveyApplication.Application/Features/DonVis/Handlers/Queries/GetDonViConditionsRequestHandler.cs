using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.Features.DonVis.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.DonVis.Handlers.Queries
{
   
    public class GetDonViConditionsRequestHandler : IRequestHandler<GetDonViConditionsRequest, BaseQuerieResponse<DonViDto>>
    {
        private readonly IDonViRepository _donViRepository;
        private readonly IMapper _mapper;
        public GetDonViConditionsRequestHandler(IDonViRepository donViRepository, IMapper mapper)
        {
            _donViRepository = donViRepository;
            _mapper = mapper;
        }

        public async Task<BaseQuerieResponse<DonViDto>> Handle(GetDonViConditionsRequest request, CancellationToken cancellationToken)
        {
            var donVis = await _donViRepository.GetByConditionsQueriesResponse(request.PageIndex, request.PageSize, x => string.IsNullOrEmpty(request.Keyword) || !string.IsNullOrEmpty(x.TenDonVi) && x.TenDonVi.Contains(request.Keyword), "");
            var result = _mapper.Map<List<DonViDto>>(donVis);
            return new BaseQuerieResponse<DonViDto>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Keyword = request.Keyword,
                TotalFilter = donVis.TotalFilter,
                Data = result
            };
        }
    }

}
