using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.DTOs.BaoCaoCauHoi;
using SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

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
                        where a.IdDotKhaoSat == request.IdDotKhaoSat &&
                              a.IdBangKhaoSat == request.IdBangKhaoSat &&
                              (request.IdLoaiHinhDonVi == null || a.IdLoaiHinhDonVi == request.IdLoaiHinhDonVi) &&
                              (request.NgayBatDau == null || b.NgayBatDau >= request.NgayBatDau) &&
                              (request.NgayKetThuc == null || b.NgayKetThuc <= request.NgayKetThuc) &&
                              !string.IsNullOrEmpty(a.CauTraLoi) &&
                              (string.IsNullOrEmpty(request.Keyword) || a.CauHoi.Contains(request.Keyword)) &&
                              a.Deleted == false
                        select a;

            var data = from a in query
                       group new { a } by new { a.IdBangKhaoSat, a.IdDotKhaoSat, a.IdDonVi, a.IdGuiEmail } into grBc
                       select new BaoCaoCauHoiChiTietDto
                       {
                           IdDonVi = grBc.Key.IdDonVi,
                           IdBangKhaoSat = grBc.Key.IdBangKhaoSat,
                           IdDotKhaoSat = grBc.Key.IdDotKhaoSat,
                           DauThoiGian = query.First(x => x.IdDonVi == grBc.Key.IdDonVi && x.IdBangKhaoSat == grBc.Key.IdBangKhaoSat && x.IdDotKhaoSat == grBc.Key.IdDotKhaoSat).DauThoiGian ?? DateTime.MinValue,
                           LstCauHoiCauTraLoi = query.Where(x => x.IdDonVi == grBc.Key.IdDonVi && x.IdBangKhaoSat == grBc.Key.IdBangKhaoSat && x.IdDotKhaoSat == grBc.Key.IdDotKhaoSat).GroupBy(x => x.LoaiCauHoi).Select(x =>
                                new CauHoiCauTraLoiChiTietDto
                                {
                                    CauHoi = x.First().CauHoi,
                                    LoaiCauHoi = x.First().LoaiCauHoi,
                                    CauTraLoi = x.Where(ctl => ctl.LoaiCauHoi == x.Key).GroupBy(ctl => new { ctl.CauTraLoi, ctl.MaCauTraLoi }).Select(ctl => ctl.Key.CauTraLoi).ToList(),
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
