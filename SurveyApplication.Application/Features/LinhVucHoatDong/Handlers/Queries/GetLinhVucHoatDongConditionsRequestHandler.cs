using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.LinhVucHoatDong;
using SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.LinhVucHoatDong.Handlers.Queries;

public class GetLinhVucHoatDongConditionsRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetLinhVucHoatDongConditionsRequest, BaseQuerieResponse<LinhVucHoatDongDto>>
{
    private readonly IMapper _mapper;

    public GetLinhVucHoatDongConditionsRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseQuerieResponse<LinhVucHoatDongDto>> Handle(GetLinhVucHoatDongConditionsRequest request,
        CancellationToken cancellationToken)
    {
        var LinhVucHoatDongs = await _surveyRepo.LinhVucHoatDong.GetByConditionsQueriesResponse(request.PageIndex,
            request.PageSize,
            x => (string.IsNullOrEmpty(request.Keyword) ||
                  (!string.IsNullOrEmpty(x.MaLinhVuc) && x.MaLinhVuc.Contains(request.Keyword)) ||
                  (!string.IsNullOrEmpty(x.TenLinhVuc) && x.TenLinhVuc.Contains(request.Keyword))) &&
                 x.Deleted == false, "");
        var result = _mapper.Map<List<LinhVucHoatDongDto>>(LinhVucHoatDongs);
        return new BaseQuerieResponse<LinhVucHoatDongDto>
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Keyword = request.Keyword,
            TotalFilter = LinhVucHoatDongs.TotalFilter,
            Data = result
        };
    }
}