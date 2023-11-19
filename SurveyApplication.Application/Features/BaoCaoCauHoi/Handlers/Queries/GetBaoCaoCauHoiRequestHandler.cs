using System.Globalization;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SurveyApplication.Application.DTOs.BaoCaoCauHoi;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Queries;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.Application.Features.BaoCaoCauHoi.Handlers.Queries;

public class GetBaoCaoCauHoiRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetBaoCaoCauHoiRequest, ThongKeBaoCaoDto>
{
    private readonly IMapper _mapper;

    public GetBaoCaoCauHoiRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<ThongKeBaoCaoDto> Handle(GetBaoCaoCauHoiRequest request, CancellationToken cancellationToken)
    {
        DateTime? ngayBatDau = null;
        if (!string.IsNullOrEmpty(request.NgayBatDau))
            ngayBatDau = DateTime.ParseExact(request.NgayBatDau, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None);

        DateTime? ngayKetThuc = null;
        if (!string.IsNullOrEmpty(request.NgayKetThuc))
            ngayKetThuc = DateTime.ParseExact(request.NgayKetThuc, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None);

        if (!string.IsNullOrEmpty(request.NgayBatDau) && !string.IsNullOrEmpty(request.NgayKetThuc))
            ngayKetThuc = ngayBatDau == ngayKetThuc
                ? ngayKetThuc.Value.AddHours(23).AddMinutes(59).AddSeconds(59)
                : ngayKetThuc.Value;

        var query = from a in _surveyRepo.BaoCaoCauHoi.GetAllQueryable()
            join b in _surveyRepo.BangKhaoSat.GetAllQueryable() on a.IdBangKhaoSat equals b.Id
            join c in _surveyRepo.DotKhaoSat.GetAllQueryable() on a.IdDotKhaoSat equals c.Id
            join d in _surveyRepo.CauHoi.GetAllQueryable() on a.IdCauHoi equals d.Id
            join e in _surveyRepo.DonVi.GetAllQueryable() on a.IdDonVi equals e.Id
            join g in _surveyRepo.LoaiHinhDonVi.GetAllQueryable() on a.IdLoaiHinhDonVi equals g.Id
            where (request.IdDotKhaoSat == 0 || a.IdDotKhaoSat == request.IdDotKhaoSat) &&
                  (request.IdBangKhaoSat == 0 || a.IdBangKhaoSat == request.IdBangKhaoSat) &&
                  (request.IdLoaiHinhDonVi == null || a.IdLoaiHinhDonVi == request.IdLoaiHinhDonVi) &&
                  (ngayBatDau == null || b.NgayBatDau.Date >= ngayBatDau.GetValueOrDefault(DateTime.MinValue).Date) &&
                  (ngayKetThuc == null ||
                   b.NgayKetThuc.Date <= ngayKetThuc.GetValueOrDefault(DateTime.MaxValue).Date) &&
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

                LoaiCauHoi = a.LoaiCauHoi,
                MaCauHoi = a.MaCauHoi,
                CauHoi = a.CauHoi,
                MaCauHoiPhu = a.MaCauHoiPhu,
                CauHoiPhu = a.CauHoiPhu,
                MaCauTraLoi = a.MaCauTraLoi,
                CauTraLoi = a.CauTraLoi
            };

        var donViDuocMoi = await (from a in _surveyRepo.GuiEmail.GetAllQueryable()
            join b in _surveyRepo.BangKhaoSat.GetAllQueryable() on a.IdBangKhaoSat equals b.Id
            where (request.IdBangKhaoSat == 0 || b.Id == request.IdBangKhaoSat) &&
                  (ngayBatDau == null || b.NgayBatDau.Date >= ngayBatDau.GetValueOrDefault(DateTime.MinValue).Date) &&
                  (ngayKetThuc == null ||
                   b.NgayKetThuc.Date <= ngayKetThuc.GetValueOrDefault(DateTime.MaxValue).Date) &&
                  a.Deleted == false
            select new GuiEmailDto
            {
                Id = a.Id
            }).CountAsync(cancellationToken);

        var donViThamGia = await (from a in _surveyRepo.KetQua.GetAllQueryable()
            join b in _surveyRepo.GuiEmail.GetAllQueryable() on a.IdGuiEmail equals b.Id
            join c in _surveyRepo.BangKhaoSat.GetAllQueryable() on b.IdBangKhaoSat equals c.Id
            where (request.IdBangKhaoSat == 0 || c.Id == request.IdBangKhaoSat) &&
                  (request.NgayBatDau == null ||
                   c.NgayBatDau.Date >= ngayBatDau.GetValueOrDefault(DateTime.MinValue).Date) &&
                  (request.NgayKetThuc == null ||
                   c.NgayKetThuc.Date <= ngayKetThuc.GetValueOrDefault(DateTime.MaxValue).Date) &&
                  c.Deleted == false
            select new KetQua
            {
                Id = c.Id
            }).CountAsync(cancellationToken);

        //TODO bắt buộc phải nhập đợt khảo sát và bảng khảo sát
        var queryBaoCao = from a in _surveyRepo.BaoCaoCauHoi.GetAllQueryable()
            join b in _surveyRepo.BangKhaoSat.GetAllQueryable() on a.IdBangKhaoSat equals b.Id
            join c in _surveyRepo.DotKhaoSat.GetAllQueryable() on a.IdDotKhaoSat equals c.Id
            join d in _surveyRepo.CauHoi.GetAllQueryable() on a.IdCauHoi equals d.Id
            join e in _surveyRepo.DonVi.GetAllQueryable() on a.IdDonVi equals e.Id
            join f in _surveyRepo.GuiEmail.GetAllQueryable() on a.IdGuiEmail equals f.Id
            join g in _surveyRepo.LoaiHinhDonVi.GetAllQueryable() on a.IdLoaiHinhDonVi equals g.Id
            where a.IdDotKhaoSat == request.IdDotKhaoSat &&
                  a.IdBangKhaoSat == request.IdBangKhaoSat &&
                  (request.IdLoaiHinhDonVi == null || a.IdLoaiHinhDonVi == request.IdLoaiHinhDonVi) &&
                  (ngayBatDau == null || b.NgayBatDau.Date >= ngayBatDau.GetValueOrDefault(DateTime.MinValue).Date) &&
                  (ngayKetThuc == null ||
                   b.NgayKetThuc.Date <= ngayKetThuc.GetValueOrDefault(DateTime.MaxValue).Date) &&
                  a.Deleted == false &&
                  f.TrangThai == (int)EnumGuiEmail.TrangThai.ThanhCong
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

                LoaiCauHoi = a.LoaiCauHoi,
                MaCauHoi = a.MaCauHoi,
                CauHoi = a.CauHoi,
                MaCauHoiPhu = a.MaCauHoiPhu,
                CauHoiPhu = a.CauHoiPhu,
                MaCauTraLoi = a.MaCauTraLoi,
                CauTraLoi = a.CauTraLoi
            };

        var groupedResults = queryBaoCao.GroupBy(g => new { g.IdCauHoi, g.CauHoi, g.LoaiCauHoi })
            .OrderBy(o => o.Key.IdCauHoi);

        var groupedDataList = await groupedResults.Select(group => new ListCauHoiTraLoi
        {
            IdCauHoi = group.Key.IdCauHoi,
            TenCauHoi = group.Key.CauHoi,
            LoaiCauHoi = group.Key.LoaiCauHoi,
            CauHoiTraLoi = group.ToList()
        }).ToListAsync(cancellationToken);

        foreach (var item in groupedDataList.Where(item => item.CauHoiTraLoi != null))
        {
            //TODO bỏ câu trả lời rỗng và câu trả lời khác (other)
            item.CauHoiTraLoi = item.CauHoiTraLoi
                .Where(x => !string.IsNullOrEmpty(x.CauTraLoi) && x.CauTraLoi != "other").ToList();
            if (item.LoaiCauHoi != (int)EnumCauHoi.Type.MultiSelectMatrix)
                // Loại bỏ các bản ghi trùng lặp trong danh sách CauHoiTraLoi
                item.CauHoiTraLoi = item.CauHoiTraLoi.GroupBy(x => x.CauTraLoi).Select(group => group.First())
                    .ToList();
            else //TODO Câu trả lời dạng chọn nhiều check khác (vì câu trả lời theo câu hỏi phụ có thể giống nhau)
                foreach (var itemCauHoiTraLoi in item.CauHoiTraLoi)
                {
                    if (string.IsNullOrEmpty(itemCauHoiTraLoi.CauTraLoi)) continue;
                    var lstCauTraLoi = JsonConvert.DeserializeObject<List<string>>(itemCauHoiTraLoi.CauTraLoi);
                    var lstCauTraLoiNew =
                        (from itemCauTraLoi in lstCauTraLoi?.Where(x => !string.IsNullOrEmpty(x)) ?? new List<string>()
                            select $"{itemCauTraLoi} ({itemCauHoiTraLoi.CauHoiPhu})").ToList();
                    itemCauHoiTraLoi.CauTraLoi = JsonConvert.SerializeObject(lstCauTraLoiNew);
                }


            foreach (var cauHoiTraLoi in item.CauHoiTraLoi)
            {
                cauHoiTraLoi.SoLuotChon = item.CauHoiTraLoi.Count(x => x.CauTraLoi == cauHoiTraLoi.CauTraLoi);
                cauHoiTraLoi.TyLe = (double)cauHoiTraLoi.SoLuotChon / item.CauHoiTraLoi.Count * 100;
            }
        }

        var lstDoiTuongThamGiaKs = await _surveyRepo.LoaiHinhDonVi.GetAllQueryable()
            .Where(x => !x.Deleted && query.Select(q => q.IdLoaiHinhDonVi).Contains(x.Id)).Select(x =>
                new DoiTuongThamGiaKs
                {
                    Ten = x.TenLoaiHinh,
                    SoLuong = query
                        .Select(cauHoiTraLoi => new { cauHoiTraLoi.IdLoaiHinhDonVi, cauHoiTraLoi.IdBangKhaoSat })
                        .GroupBy(gr => new { gr.IdLoaiHinhDonVi, gr.IdBangKhaoSat })
                        .Count(q => q.Key.IdLoaiHinhDonVi == x.Id)
                }).Take(5).OrderByDescending(x => x.SoLuong).ToListAsync(cancellationToken);

        return new ThongKeBaoCaoDto
        {
            CountDonViMoi = donViDuocMoi,
            CountDonViTraLoi = donViThamGia,
            ListCauHoiTraLoi = groupedDataList,
            LstDoiTuongThamGiaKs = lstDoiTuongThamGiaKs
        };
    }
}