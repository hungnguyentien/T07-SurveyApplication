using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.Application.Features.BangKhaoSats.Handlers.Queries;

public class GetBangKhaoSatDetailRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetBangKhaoSatDetailRequest, BangKhaoSatDto>
{
    private readonly IMapper _mapper;

    public GetBangKhaoSatDetailRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BangKhaoSatDto> Handle(GetBangKhaoSatDetailRequest request, CancellationToken cancellationToken)
    {
        var bks = await _surveyRepo.BangKhaoSat.GetById(request.Id);
        var rs = _mapper.Map<BangKhaoSatDto>(bks);
        var lstKhaoSatCauHoi = from a in _surveyRepo.BangKhaoSatCauHoi.GetAllQueryable()
            join b in _surveyRepo.CauHoi.GetAllQueryable() on a.IdCauHoi equals b.Id
            where a.IdBangKhaoSat == bks.Id && b.ActiveFlag == (int)EnumCommon.ActiveFlag.Active && !a.Deleted &&
                  !b.Deleted
            select new BangKhaoSatCauHoiDto
            {
                Id = a.Id,
                IdBangKhaoSat = a.IdBangKhaoSat,
                IdCauHoi = a.IdCauHoi,
                Priority = a.Priority,
                IsRequired = a.IsRequired,
                PanelTitle = a.PanelTitle,
                TieuDe = b.TieuDe,
                MaCauHoi = b.MaCauHoi
            };
        rs.BangKhaoSatCauHoi = await lstKhaoSatCauHoi.ToListAsync(cancellationToken);
        return rs;
    }
}