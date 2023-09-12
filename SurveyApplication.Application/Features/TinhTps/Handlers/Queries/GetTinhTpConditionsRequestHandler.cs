using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var tinhTP = await _surveyRepo.LoaiHinhDonVi.GetByConditionsQueriesResponse(request.PageIndex, request.PageSize, x => string.IsNullOrEmpty(request.Keyword) || !string.IsNullOrEmpty(x.TenLoaiHinh) && x.TenLoaiHinh.Contains(request.Keyword), "");
            var result = _mapper.Map<List<TinhTpDto>>(tinhTP);
            return new BaseQuerieResponse<TinhTpDto>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Keyword = request.Keyword,
                TotalFilter = tinhTP.TotalFilter,
                Data = result
            };
        }
    }

}
