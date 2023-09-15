using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.Enums;
using SurveyApplication.Application.Features.CauHoi.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility;

namespace SurveyApplication.Application.Features.CauHoi.Handlers.Queries;

public class GetCauHoiConditionsRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetCauHoiConditionsRequest, BaseQuerieResponse<CauHoiDto>>
{
    private readonly IMapper _mapper;

    public GetCauHoiConditionsRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseQuerieResponse<CauHoiDto>> Handle(GetCauHoiConditionsRequest request,
        CancellationToken cancellationToken)
    {
        var lstCauHoi = await _surveyRepo.CauHoi.GetByConditionsQueriesResponse(request.PageIndex, request.PageSize,
            x => (string.IsNullOrEmpty(request.Keyword) || x.TieuDe.Contains(request.Keyword)) &&
                 x.ActiveFlag == (int)EnumCommon.ActiveFlag.Active && !x.Deleted, request.OrderBy ?? "");
        var result = _mapper.Map<List<CauHoiDto>>(lstCauHoi);
        result.ForEach(x =>
            x.LoaiCauHoiText = EnumUltils.GetDescription<EnumCauHoi.Type>().First(t => (int)t.Key == x.LoaiCauHoi)
                .Value);
        return new BaseQuerieResponse<CauHoiDto>
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Keyword = request.Keyword,
            TotalFilter = lstCauHoi.TotalFilter,
            Data = result
        };
    }
}