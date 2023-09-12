using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.XaPhuong;
using SurveyApplication.Application.Features.XaPhuongs.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.XaPhuongs.Handlers.Queries
{
   
    public class GetXaPhuongConditionsRequestHandler : BaseMasterFeatures, IRequestHandler<GetXaPhuongConditionsRequest, BaseQuerieResponse<XaPhuongDto>>
    {
        private readonly IMapper _mapper;
        public GetXaPhuongConditionsRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseQuerieResponse<XaPhuongDto>> Handle(GetXaPhuongConditionsRequest request, CancellationToken cancellationToken)
        {
            var XaPhuongs = await _surveyRepo.XaPhuong.GetByConditionsQueriesResponse(request.PageIndex, request.PageSize, x => string.IsNullOrEmpty(request.Keyword) || !string.IsNullOrEmpty(x.Name) && x.Name.Contains(request.Keyword), "");
            var result = _mapper.Map<List<XaPhuongDto>>(XaPhuongs);
            return new BaseQuerieResponse<XaPhuongDto>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Keyword = request.Keyword,
                TotalFilter = XaPhuongs.TotalFilter,
                Data = result
            };
        }
    }

}
