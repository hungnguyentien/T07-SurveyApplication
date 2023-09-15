using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Handlers.Queries
{
   
    public class GetLoaiHinhDonViConditionsRequestHandler : BaseMasterFeatures, IRequestHandler<GetLoaiHinhDonViConditionsRequest, BaseQuerieResponse<LoaiHinhDonViDto>>
    {
        private readonly IMapper _mapper;
        public GetLoaiHinhDonViConditionsRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseQuerieResponse<LoaiHinhDonViDto>> Handle(GetLoaiHinhDonViConditionsRequest request, CancellationToken cancellationToken)
        {
            var LoaiHinhDonVis = await _surveyRepo.LoaiHinhDonVi.GetByConditionsQueriesResponse(request.PageIndex, request.PageSize, x => (string.IsNullOrEmpty(request.Keyword) || !string.IsNullOrEmpty(x.TenLoaiHinh) && x.TenLoaiHinh.Contains(request.Keyword)) && x.Deleted == false, "");
            var result = _mapper.Map<List<LoaiHinhDonViDto>>(LoaiHinhDonVis);
            return new BaseQuerieResponse<LoaiHinhDonViDto>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Keyword = request.Keyword,
                TotalFilter = LoaiHinhDonVis.TotalFilter,
                Data = result
            };
        }
    }

}
