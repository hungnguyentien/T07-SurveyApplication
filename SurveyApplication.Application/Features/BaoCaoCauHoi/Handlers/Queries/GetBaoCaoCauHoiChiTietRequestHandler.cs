using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.DTOs.BaoCaoCauHoi;
using SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.Application.Features.BaoCaoCauHoi.Handlers.Queries
{
    public class GetBaoCaoCauHoiChiTietRequestHandler : BaseMasterFeatures, IRequestHandler<GetBaoCaoCauHoiChiTietRequest, BaseQuerieResponse<BaoCaoCauHoiChiTietDto>>
    {
        public GetBaoCaoCauHoiChiTietRequestHandler(ISurveyRepositoryWrapper surveyRepository) : base(surveyRepository)
        {
        }

        public async Task<BaseQuerieResponse<BaoCaoCauHoiChiTietDto>> Handle(GetBaoCaoCauHoiChiTietRequest request, CancellationToken cancellationToken)
        {
            var query = from a in _surveyRepo.BaoCaoCauHoi.GetAllQueryable()
                        join b in _surveyRepo.BangKhaoSat.GetAllQueryable() on a.IdBangKhaoSat equals b.Id
                        join c in _surveyRepo.GuiEmail.GetAllQueryable() on a.IdGuiEmail equals c.Id
                        where a.IdDotKhaoSat == request.IdDotKhaoSat &&
                              a.IdBangKhaoSat == request.IdBangKhaoSat &&
                              (request.IdLoaiHinhDonVi == null || a.IdLoaiHinhDonVi == request.IdLoaiHinhDonVi) &&
                              (request.NgayBatDau == null || b.NgayBatDau.Date >= request.NgayBatDau.GetValueOrDefault(DateTime.MinValue).Date) &&
                              (request.NgayKetThuc == null || b.NgayKetThuc.Date <= request.NgayKetThuc.GetValueOrDefault(DateTime.MaxValue).Date) &&
                              !string.IsNullOrEmpty(a.CauTraLoi) &&
                              (string.IsNullOrEmpty(request.Keyword) || a.CauHoi.Contains(request.Keyword)) &&
                              !a.Deleted &&
                              c.TrangThai == (int)EnumGuiEmail.TrangThai.ThanhCong
                        select a;
            
            var data = from a in query
                       group new { a } by new { a.IdBangKhaoSat, a.IdDotKhaoSat, a.IdDonVi, a.IdGuiEmail } into grBc
                       select new BaoCaoCauHoiChiTietDto
                       {
                           IdDonVi = grBc.Key.IdDonVi,
                           IdBangKhaoSat = grBc.Key.IdBangKhaoSat,
                           IdDotKhaoSat = grBc.Key.IdDotKhaoSat,
                           DauThoiGian = query.First(x => x.IdDonVi == grBc.Key.IdDonVi && x.IdBangKhaoSat == grBc.Key.IdBangKhaoSat && x.IdDotKhaoSat == grBc.Key.IdDotKhaoSat).DauThoiGian ?? DateTime.MinValue,
                           LstCauHoiCauTraLoi = query.GroupBy(x => new {x.LoaiCauHoi, x.CauHoi}).Select(x =>
                                new CauHoiCauTraLoiChiTietDto
                                {
                                    CauHoi = x.First().CauHoi,
                                    LoaiCauHoi = x.First().LoaiCauHoi,
                                    CauTraLoi = x.Where(ctl => ctl.LoaiCauHoi == x.Key.LoaiCauHoi && ctl.CauHoi == x.Key.CauHoi).GroupBy(ctl => new { ctl.CauTraLoi, ctl.MaCauTraLoi }).Select(ctl => ctl.Key.CauTraLoi).ToList(),
                                }).ToList()
                       };

            var totalCount = (await data.ToListAsync(cancellationToken: cancellationToken)).LongCount();
            var pageResults = await data.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .ToListAsync(cancellationToken: cancellationToken);
            return new BaseQuerieResponse<BaoCaoCauHoiChiTietDto>
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
