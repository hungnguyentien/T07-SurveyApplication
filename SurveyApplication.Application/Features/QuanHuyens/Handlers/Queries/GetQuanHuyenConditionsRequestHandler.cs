using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.QuanHuyen;
using SurveyApplication.Application.Features.QuanHuyens.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.QuanHuyens.Handlers.Queries
{
   
    public class GetQuanHuyenConditionsRequestHandler : BaseMasterFeatures, IRequestHandler<GetQuanHuyenConditionsRequest, BaseQuerieResponse<QuanHuyenDto>>
    {
        private readonly IMapper _mapper;
        public GetQuanHuyenConditionsRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseQuerieResponse<QuanHuyenDto>> Handle(GetQuanHuyenConditionsRequest request, CancellationToken cancellationToken)
        {
            var QuanHuyens = await _surveyRepo.QuanHuyen.GetByConditionsQueriesResponse(request.PageIndex, request.PageSize, x => string.IsNullOrEmpty(request.Keyword) || !string.IsNullOrEmpty(x.Name) && x.Name.Contains(request.Keyword), "");
            var result = _mapper.Map<List<QuanHuyenDto>>(QuanHuyens);
            return new BaseQuerieResponse<QuanHuyenDto>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Keyword = request.Keyword,
                TotalFilter = QuanHuyens.TotalFilter,
                Data = result
            };
        }
    }

}
