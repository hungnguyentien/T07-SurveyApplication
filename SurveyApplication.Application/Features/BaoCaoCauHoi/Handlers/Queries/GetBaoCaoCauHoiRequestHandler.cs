using MediatR;
using SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Queries;
using AutoMapper;
using SurveyApplication.Application.DTOs.BaoCaoCauHoi;
using SurveyApplication.Domain.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Domain;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Application.DTOs.GuiEmail;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;

namespace SurveyApplication.Application.Features.BaoCaoCauHoi.Handlers.Queries
{
    public class GetBaoCaoCauHoiRequestHandler : BaseMasterFeatures, IRequestHandler<GetBaoCaoCauHoiRequest, ThongKeBaoCaoDto>
    {
        private readonly IMapper _mapper;

        public GetBaoCaoCauHoiRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<ThongKeBaoCaoDto> Handle(GetBaoCaoCauHoiRequest request, CancellationToken cancellationToken)
        {
            DateTime? ngayBatDau = null;
            if (!string.IsNullOrEmpty(request.NgayBatDau))
                ngayBatDau = DateTime.ParseExact(request.NgayBatDau, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);

            DateTime? ngayKetThuc = null;
            if (!string.IsNullOrEmpty(request.NgayKetThuc))
                ngayKetThuc = DateTime.ParseExact(request.NgayKetThuc, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                
            var query = from a in _surveyRepo.BaoCaoCauHoi.GetAllQueryable()
                        join b in _surveyRepo.BangKhaoSat.GetAllQueryable() on a.IdBangKhaoSat equals b.Id
                        join c in _surveyRepo.DotKhaoSat.GetAllQueryable() on a.IdDotKhaoSat equals c.Id
                        join d in _surveyRepo.CauHoi.GetAllQueryable() on a.IdCauHoi equals d.Id
                        join e in _surveyRepo.DonVi.GetAllQueryable() on a.IdDonVi equals e.Id
                        join g in _surveyRepo.LoaiHinhDonVi.GetAllQueryable() on a.IdLoaiHinhDonVi equals g.Id

                        where (request.IdDotKhaoSat == 0 || a.IdDotKhaoSat == request.IdDotKhaoSat) &&
                             (request.IdBangKhaoSat == 0 || a.IdBangKhaoSat == request.IdBangKhaoSat) &&
                             (request.IdLoaiHinhDonVi == null || a.IdLoaiHinhDonVi == request.IdLoaiHinhDonVi) &&
                             (ngayBatDau == null || b.NgayBatDau >= ngayBatDau) &&
                             (ngayKetThuc == null || b.NgayKetThuc <= ngayKetThuc) &&
                             a.Deleted == false

                        select new CauHoiTraLoi
                        {
                            IdBangKhaoSat = b.Id,
                            IdDotKhaoSat = c.Id,
                            IdCauHoi = d.Id,
                            IdLoaiHinhDonVi = g.Id,
                            IdDonVi = e.Id,

                            TenDaiDienCq = e.TenDonVi,
                            TenLoaiHinhDonVi = g.TenLoaiHinh,
                            DiaChi = e.DiaChi,

                            DauThoiGian = a.DauThoiGian,
                            LoaiCauHoi = a.LoaiCauHoi,
                            MaCauHoi = a.MaCauHoi,
                            CauHoi = a.CauHoi,
                            MaCauHoiPhu = a.MaCauHoiPhu,
                            CauHoiPhu = a.CauHoiPhu,
                            MaCauTraLoi = a.MaCauTraLoi,
                            CauTraLoi = a.CauTraLoi,
                        };

            var donViDuocMoi = await (from a in _surveyRepo.GuiEmail.GetAllQueryable()
                                      join b in _surveyRepo.BangKhaoSat.GetAllQueryable() on a.IdBangKhaoSat equals b.Id
                                      where (request.IdBangKhaoSat == 0 || b.Id == request.IdBangKhaoSat) &&
                                          (request.NgayBatDau == null || b.NgayBatDau >= ngayBatDau) &&
                                          (request.NgayKetThuc == null || b.NgayKetThuc <= ngayKetThuc) &&
                                          a.Deleted == false
                                      select new GuiEmailDto
                                      {
                                          Id = a.Id
                                      }).CountAsync(cancellationToken: cancellationToken);

            var donViThamGia = await (from a in _surveyRepo.KetQua.GetAllQueryable()
                                      join b in _surveyRepo.GuiEmail.GetAllQueryable() on a.IdGuiEmail equals b.Id
                                      join c in _surveyRepo.BangKhaoSat.GetAllQueryable() on b.IdBangKhaoSat equals c.Id
                                      where (request.IdBangKhaoSat == 0 || c.Id == request.IdBangKhaoSat) &&
                                          (request.NgayBatDau == null || c.NgayBatDau >= ngayBatDau) &&
                                          (request.NgayKetThuc == null || c.NgayKetThuc <= ngayKetThuc) &&
                                          c.Deleted == false
                                      select new KetQua
                                      {
                                          Id = c.Id
                                      }).CountAsync(cancellationToken: cancellationToken);

            var groupedResults = query.GroupBy(g => new { g.IdCauHoi, g.CauHoi, g.DauThoiGian }).OrderBy(o => o.Key.IdCauHoi);

            var groupedDataList = await groupedResults.Select(group => new ListCauHoiTraLoi
            {
                IdCauHoi = group.Key.IdCauHoi,
                TenCauHoi = group.Key.CauHoi,
                DauThoiGian = group.Key.DauThoiGian,
                CauHoiTraLoi = group.ToList()
            }).ToListAsync(cancellationToken: cancellationToken);

            foreach (var item in groupedDataList)
            {
                foreach (var cauHoiTraLoi in item.CauHoiTraLoi)
                {
                    cauHoiTraLoi.SoLuotChon = item.CauHoiTraLoi.Count(x => x.CauTraLoi == cauHoiTraLoi.CauTraLoi);
                    cauHoiTraLoi.TyLe = (double)cauHoiTraLoi.SoLuotChon / item.CauHoiTraLoi.Count * 100;
                }

                // Loại bỏ các bản ghi trùng lặp trong danh sách CauHoiTraLoi
                item.CauHoiTraLoi = item.CauHoiTraLoi.GroupBy(x => x.CauTraLoi).Select(group => group.First()).ToList();
            }

            return new ThongKeBaoCaoDto
            {
                CountDonViSo = await query.CountAsync(x => x.IdLoaiHinhDonVi == 1),
                CountDonViBo = await query.CountAsync(x => x.IdLoaiHinhDonVi == 2),
                CountDonViNganh = await query.CountAsync(x => x.IdLoaiHinhDonVi == 3),
                CountDonViMoi = donViDuocMoi,
                CountDonViTraLoi = donViThamGia,
                ListCauHoiTraLoi = groupedDataList,
            };
        }
    }
}
