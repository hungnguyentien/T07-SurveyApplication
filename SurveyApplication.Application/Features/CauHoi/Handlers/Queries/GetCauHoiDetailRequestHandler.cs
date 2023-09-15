using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.Features.CauHoi.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.CauHoi.Handlers.Queries;

public class GetCauHoiDetailRequestHandler : BaseMasterFeatures, IRequestHandler<GetCauHoiDetailRequest, CauHoiDto>
{
    private readonly IMapper _mapper;

    public GetCauHoiDetailRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<CauHoiDto> Handle(GetCauHoiDetailRequest request, CancellationToken cancellationToken)
    {
        var cauHoi = await _surveyRepo.CauHoi.GetById(request.Id);
        var lstCot = await _surveyRepo.Cot.GetAllListAsync(x => x.IdCauHoi == cauHoi.Id);
        var lstHang = await _surveyRepo.Hang.GetAllListAsync(x => x.IdCauHoi == cauHoi.Id);
        var result = _mapper.Map<CauHoiDto>(cauHoi);
        result.LstCot = _mapper.Map<List<CotDto>>(lstCot.Where(c => c.IdCauHoi == cauHoi.Id).Select(c => c));
        result.LstHang = _mapper.Map<List<HangDto>>(lstHang.Where(h => h.IdCauHoi == cauHoi.Id).Select(h => h));
        return result;
    }
}