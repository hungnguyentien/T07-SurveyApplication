using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.DTOs.BaoCaoCauHoi;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Queries;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.Application.Features.BaoCaoCauHoi.Handlers.Queries
{
    public class GetDashBoardRequestHandler : BaseMasterFeatures, IRequestHandler<GetDashBoardRequest, DashBoardDto>
    {
        private readonly IMapper _mapper;
        public GetDashBoardRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }
        public async Task<DashBoardDto> Handle(GetDashBoardRequest request, CancellationToken cancellationToken)
        {
            var ngayBatDau = request.NgayBatDau.GetValueOrDefault(DateTime.MaxValue).Date;
            var ngayuKetThuc = request.NgayKetThuc.GetValueOrDefault(DateTime.MinValue).Date;
            var tongDotKhaoSat = await (from a in _surveyRepo.DotKhaoSat.GetAllQueryable()
                                        join b in _surveyRepo.BangKhaoSat.GetAllQueryable() on a.Id equals b.IdDotKhaoSat
                                        where (request.NgayBatDau == null || b.NgayBatDau.Date >= ngayBatDau) &&
                                            (request.NgayKetThuc == null || b.NgayKetThuc.Date <= ngayuKetThuc) &&
                                            a.Deleted == false
                                        select new DotKhaoSatDto
                                        {
                                            Id = a.Id
                                        }).CountAsync(cancellationToken: cancellationToken);

            var tongBangKhaoSat = await (from a in _surveyRepo.BangKhaoSat.GetAllQueryable()
                                         where (request.NgayBatDau == null || a.NgayBatDau.Date >= ngayBatDau) &&
                                             (request.NgayKetThuc == null || a.NgayKetThuc.Date <= ngayuKetThuc) &&
                                             a.Deleted == false
                                         select new BangKhaoSatDto
                                         {
                                             Id = a.Id
                                         }).CountAsync(cancellationToken: cancellationToken);



            var tongThamGiaKhaoSat = await (from a in _surveyRepo.BangKhaoSat.GetAllQueryable()
                                            join b in _surveyRepo.GuiEmail.GetAllQueryable() on a.Id equals b.IdBangKhaoSat
                                            join c in _surveyRepo.KetQua.GetAllQueryable() on b.Id equals c.IdGuiEmail
                                            where (request.NgayBatDau == null || a.NgayBatDau.Date >= ngayBatDau) &&
                                             (request.NgayKetThuc == null || a.NgayKetThuc.Date <= ngayuKetThuc) &&
                                             c.Deleted == false && c.TrangThai == (int)EnumKetQua.TrangThai.HoanThanh &&
                                             b.TrangThai == (int)EnumGuiEmail.TrangThai.ThanhCong
                                            select new KetQua
                                            {
                                                Id = c.Id
                                            }).CountAsync(cancellationToken: cancellationToken);

            var thamGiaKhaoSat = from a in _surveyRepo.BangKhaoSat.GetAllQueryable()
                                 join b in _surveyRepo.GuiEmail.GetAllQueryable() on a.Id equals b.IdBangKhaoSat
                                 join c in _surveyRepo.KetQua.GetAllQueryable() on b.Id equals c.IdGuiEmail
                                 where !a.Deleted && c.TrangThai == (int)EnumKetQua.TrangThai.HoanThanh &&
                                       b.TrangThai == (int)EnumGuiEmail.TrangThai.ThanhCong &&
                                       (request.NgayBatDau == null || a.NgayBatDau.Date >= ngayBatDau) &&
                                       (request.NgayKetThuc == null || a.NgayKetThuc.Date <= ngayuKetThuc)
                                 select new { BangKhoaSat = a, b.IdDonVi };

            var khaoSatTheoNhom = from a in _surveyRepo.LoaiHinhDonVi.GetAllQueryable()
                                  join b in thamGiaKhaoSat on a.Id equals b.BangKhoaSat.IdLoaiHinh
                                  where !a.Deleted
                                  select new
                                  {
                                      IdBangKhaoSat = b.BangKhoaSat.Id,
                                      a.TenLoaiHinh,
                                      IdLoaiHinh = a.Id
                                  };

            var khaoSatTheoDot = from a in _surveyRepo.DotKhaoSat.GetAllQueryable()
                                 join b in thamGiaKhaoSat on a.Id equals b.BangKhoaSat.IdDotKhaoSat
                                 where !a.Deleted
                                 select new
                                 {
                                     IdBangKhaoSat = b.BangKhoaSat.Id,
                                     a.TenDotKhaoSat,
                                     IdDotKhaoSat = a.Id
                                 };

            var khaoSatTinhTp = await (from a in _surveyRepo.TinhTp.GetAllQueryable()
                                       join b in _surveyRepo.DonVi.GetAllQueryable() on a.Id equals b.IdTinhTp
                                       join c in thamGiaKhaoSat on b.Id equals c.IdDonVi
                                       join d in _surveyRepo.TinhTp.GetAllQueryable() on b.IdTinhTp equals d.Id
                                       where !d.Deleted
                                       select new DonViTinhTpDto
                                       {
                                           Id = c.IdDonVi,
                                           IdTinhTp = d.Id,
                                           TinhTp = d.Name,
                                       }).ToListAsync(cancellationToken: cancellationToken);

            var tongKhaoSatTinhTp = khaoSatTinhTp.GroupBy(g => new { g.IdTinhTp, g.TinhTp }).OrderByDescending(group => group.Count()).ToList();
            var groupedDataList = tongKhaoSatTinhTp.Select(group => new ListTinhTp
            {
                IdTinhTp = group.Key.IdTinhTp,
                CountTinhTp = group.ToList().Count,
                TenTinhTp = group.Key.TinhTp,
                ListDonVi = group.ToList()
            }).ToList();

            return new DashBoardDto
            {
                CountDotKhaoSat = tongDotKhaoSat,
                CountBangKhaoSat = tongBangKhaoSat,
                CountThamGia = tongThamGiaKhaoSat,
                ListTinhTp = groupedDataList,
                LstCountDonViByLoaiHinh = await khaoSatTheoNhom.GroupBy(x => x.IdLoaiHinh).Select(n => new CountDonViByLoaiHinh
                {
                    Ten = n.First().TenLoaiHinh,
                    Count = n.Select(x => x.IdBangKhaoSat).Count()
                }).Take(5).OrderByDescending(x => x.Count).ToListAsync(cancellationToken: cancellationToken),
                LstCountDot = await khaoSatTheoDot.GroupBy(x => x.IdDotKhaoSat).Select(n => new CountDot
                {
                    Ten = n.First().TenDotKhaoSat,
                    Count = khaoSatTheoDot.Count(x => x.IdDotKhaoSat == n.Key)
                }).Take(5).OrderByDescending(x => x.Count).ToListAsync(cancellationToken: cancellationToken)
            };
        }
    }
}
