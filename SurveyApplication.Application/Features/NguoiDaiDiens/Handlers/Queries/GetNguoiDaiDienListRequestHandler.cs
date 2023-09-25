using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.NguoiDaiDien;
using SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Handlers.Queries;

public class GetNguoiDaiDienListRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetNguoiDaiDienListRequest, List<NguoiDaiDienDto>>
{
    private readonly IMapper _mapper;

    public GetNguoiDaiDienListRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<List<NguoiDaiDienDto>> Handle(GetNguoiDaiDienListRequest request,
        CancellationToken cancellationToken)
    {
        var NguoiDaiDiens = await _surveyRepo.NguoiDaiDien.GetAllListAsync(x => !x.Deleted);
        return _mapper.Map<List<NguoiDaiDienDto>>(NguoiDaiDiens);
    }
}