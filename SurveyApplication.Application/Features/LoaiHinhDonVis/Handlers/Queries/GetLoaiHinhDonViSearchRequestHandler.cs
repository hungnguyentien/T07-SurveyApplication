using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Handlers.Queries;

public class GetLoaiHinhDonViSearchRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetLoaiHinhDonViSearchRequest, List<LoaiHinhDonViDto>>
{
    private readonly IMapper _mapper;

    public GetLoaiHinhDonViSearchRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<List<LoaiHinhDonViDto>> Handle(GetLoaiHinhDonViSearchRequest request,
        CancellationToken cancellationToken)
    {
        var loaiHinhDonVis = await _surveyRepo.LoaiHinhDonVi.GetByConditions(request.PageIndex, request.PageSize,
            x => string.IsNullOrEmpty(request.Keyword) ||
                 (!string.IsNullOrEmpty(x.TenLoaiHinh) && x.TenLoaiHinh.Contains(request.Keyword)), x => x.Created);
        return _mapper.Map<List<LoaiHinhDonViDto>>(loaiHinhDonVis);
    }
}