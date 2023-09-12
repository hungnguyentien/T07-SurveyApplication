using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Application.Features.GuiEmail.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.GuiEmail.Handlers.Queries;

public class GetGuiEmailConditionsRequestHandler : BaseMasterFeatures, IRequestHandler<GetGuiEmailConditionsRequest, BaseQuerieResponse<GuiEmailDto>>
{
    public GetGuiEmailConditionsRequestHandler(ISurveyRepositoryWrapper surveyRepository) : base(
        surveyRepository)
    {
    }

    public async Task<BaseQuerieResponse<GuiEmailDto>> Handle(GetGuiEmailConditionsRequest request,
        CancellationToken cancellationToken)
    {
        var query = from d in _surveyRepo.GuiEmail.GetAllQueryable()
                    join b in _surveyRepo.BangKhaoSat.GetAllQueryable()
                        on d.IdBangKhaoSat equals b.Id
                    where d.MaGuiEmail.Contains(request.Keyword) || b.TenBangKhaoSat.Contains(request.Keyword)
                    select new GuiEmailDto
                    {
                        Id = d.Id,
                        MaGuiEmail = d.MaGuiEmail,
                        IdBangKhaoSat = b.Id,
                        TenBangKhaoSat = b.TenBangKhaoSat,
                        DiaChiNhan = d.DiaChiNhan,
                        TieuDe = d.TieuDe,
                        NoiDung = d.NoiDung,

                        TrangThai = d.TrangThai,
                        ThoiGian = d.ThoiGian
                    };
        var totalCount = await query.LongCountAsync(cancellationToken: cancellationToken);
        var pageResults = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
            .ToListAsync(cancellationToken: cancellationToken);
        return new BaseQuerieResponse<GuiEmailDto>
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Keyword = request.Keyword,
            TotalFilter = totalCount,
            Data = pageResults
        };
    }
}