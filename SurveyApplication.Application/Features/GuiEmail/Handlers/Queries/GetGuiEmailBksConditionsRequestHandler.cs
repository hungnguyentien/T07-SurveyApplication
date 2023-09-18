using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Application.Features.GuiEmail.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.Application.Features.GuiEmail.Handlers.Queries
{
    public class GetGuiEmailBksConditionsRequestHandler : BaseMasterFeatures, IRequestHandler<GetGuiEmailBksConditionsRequest, BaseQuerieResponse<GuiEmailBksDto>>
    {
        public GetGuiEmailBksConditionsRequestHandler(ISurveyRepositoryWrapper surveyRepository) : base(surveyRepository)
        {
        }

        public async Task<BaseQuerieResponse<GuiEmailBksDto>> Handle(GetGuiEmailBksConditionsRequest request,
            CancellationToken cancellationToken)
        {
            var query = from a in _surveyRepo.BangKhaoSat.GetAllQueryable().AsNoTracking()
                        join b in _surveyRepo.GuiEmail.GetAllQueryable().AsNoTracking() on a.Id equals b.IdBangKhaoSat
                        where !a.Deleted && !b.Deleted
                        && string.IsNullOrEmpty(request.Keyword) || a.TenBangKhaoSat.Contains(request.Keyword) || a.MaBangKhaoSat.Contains(request.Keyword)
                        select new
                        {
                            a.MaBangKhaoSat,
                            a.TenBangKhaoSat,
                            a.NgayBatDau,
                            a.NgayKetThuc,
                            b.IdBangKhaoSat,
                            b.TrangThai,
                        };
            var data = from a in query
                       group new { a } by new { a.IdBangKhaoSat, a.NgayBatDau, a.NgayKetThuc, a.MaBangKhaoSat, a.TenBangKhaoSat } into gbks
                       select new GuiEmailBksDto
                       {
                           IdBangKhaoSat = gbks.Key.IdBangKhaoSat,
                           MaBangKhaoSat = gbks.Key.MaBangKhaoSat,
                           TenBangKhaoSat = gbks.Key.TenBangKhaoSat,
                           CountSendEmail = query.Count(x => x.IdBangKhaoSat == gbks.Key.IdBangKhaoSat),
                           CountSendThanhCong = query.Count(x => x.IdBangKhaoSat == gbks.Key.IdBangKhaoSat && x.TrangThai == (int)EnumGuiEmail.TrangThai.ThanhCong),
                           CountSendLoi = query.Count(x => x.IdBangKhaoSat == gbks.Key.IdBangKhaoSat && x.TrangThai == (int)EnumGuiEmail.TrangThai.GuiLoi),
                           CountSendThuHoi = query.Count(x => x.IdBangKhaoSat == gbks.Key.IdBangKhaoSat && x.TrangThai == (int)EnumGuiEmail.TrangThai.ThuHoi),
                           NgayBatDau = gbks.Key.NgayBatDau,
                           NgayKetThuc = gbks.Key.NgayKetThuc,
                       };
            var totalCount = await data.LongCountAsync(cancellationToken: cancellationToken);
            var pageResults = await data.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .ToListAsync(cancellationToken: cancellationToken);
            return new BaseQuerieResponse<GuiEmailBksDto>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Keyword = request.Keyword,
                TotalFilter = totalCount,
                Data = pageResults
            };
        }
    }
}
