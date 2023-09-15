using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.NguoiDaiDien;
using SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Handlers.Queries;

public class GetNguoiDaiDienConditionsRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetNguoiDaiDienConditionsRequest, BaseQuerieResponse<NguoiDaiDienDto>>
{
    private readonly IMapper _mapper;

    public GetNguoiDaiDienConditionsRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseQuerieResponse<NguoiDaiDienDto>> Handle(GetNguoiDaiDienConditionsRequest request,
        CancellationToken cancellationToken)
    {
        var NguoiDaiDiens = await _surveyRepo.NguoiDaiDien.GetByConditionsQueriesResponse(request.PageIndex,
            request.PageSize,
            x => string.IsNullOrEmpty(request.Keyword) ||
                 (!string.IsNullOrEmpty(x.HoTen) && x.HoTen.Contains(request.Keyword)), "");
        var result = _mapper.Map<List<NguoiDaiDienDto>>(NguoiDaiDiens);
        return new BaseQuerieResponse<NguoiDaiDienDto>
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Keyword = request.Keyword,
            TotalFilter = NguoiDaiDiens.TotalFilter,
            Data = result
        };
    }
}