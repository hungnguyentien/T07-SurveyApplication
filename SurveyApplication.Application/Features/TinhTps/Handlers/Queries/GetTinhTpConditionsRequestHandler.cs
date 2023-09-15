using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.TinhTp;
using SurveyApplication.Application.Features.TinhTps.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.TinhTps.Handlers.Queries
{
   
    public class GetTinhTpConditionsRequestHandler : BaseMasterFeatures, IRequestHandler<GetTinhTpConditionsRequest, BaseQuerieResponse<TinhTpDto>>
    {
        private readonly IMapper _mapper;
        public GetTinhTpConditionsRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseQuerieResponse<TinhTpDto>> Handle(GetTinhTpConditionsRequest request, CancellationToken cancellationToken)
        {
            var TinhTps = await _surveyRepo.TinhTp.GetByConditionsQueriesResponse(request.PageIndex, request.PageSize, x => (string.IsNullOrEmpty(request.Keyword) || !string.IsNullOrEmpty(x.Name) && x.Name.Contains(request.Keyword)) && x.Deleted == false, "");
            var result = _mapper.Map<List<TinhTpDto>>(TinhTps);
            return new BaseQuerieResponse<TinhTpDto>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Keyword = request.Keyword,
                TotalFilter = TinhTps.TotalFilter,
                Data = result
            };
        }
    }

}
