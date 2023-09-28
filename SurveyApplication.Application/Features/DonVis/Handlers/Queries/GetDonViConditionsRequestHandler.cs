using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.Features.DonVis.Requests.Queries;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using System.Linq;

namespace SurveyApplication.Application.Features.DonVis.Handlers.Queries;

public class GetDonViConditionsRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetDonViConditionsRequest, BaseQuerieResponse<DonViDto>>
{
    private readonly IMapper _mapper;

    public GetDonViConditionsRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseQuerieResponse<DonViDto>> Handle(GetDonViConditionsRequest request, CancellationToken cancellationToken)
    {
        var query = from d in _surveyRepo.DonVi.GetAllQueryable()
                    join b in _surveyRepo.NguoiDaiDien.GetAllQueryable()
                    on d.Id equals b.IdDonVi
                    join o in _surveyRepo.LoaiHinhDonVi.GetAllQueryable()
                    on d.IdLoaiHinh equals o.Id
                    join s in _surveyRepo.LinhVucHoatDong.GetAllQueryable()
                    on d.IdLinhVuc equals s.Id into linhVucGroup
                    from lv in linhVucGroup.DefaultIfEmpty()

                    join tinh in _surveyRepo.TinhTp.GetAllQueryable()
                        on d.IdTinhTp equals tinh.Id into tinhGroup
                    from tinhTp in tinhGroup.DefaultIfEmpty()

                    join quan in _surveyRepo.QuanHuyen.GetAllQueryable()
                        on d.IdQuanHuyen equals quan.Id into quanGroup
                    from quanHuyen in quanGroup.DefaultIfEmpty()

                    join xa in _surveyRepo.XaPhuong.GetAllQueryable()
                        on d.IdXaPhuong equals xa.Id into xaGroup
                    from xaPhuong in xaGroup.DefaultIfEmpty()

                    where (d.MaDonVi.Contains(request.Keyword) || d.TenDonVi.Contains(request.Keyword) ||
                         d.DiaChi.Contains(request.Keyword) || b.HoTen.Contains(request.Keyword)) &&
                         d.Deleted == false
                    select new DonViDto
                    {
                        IdLinhVuc = lv != null ? lv.Id : null,
                        IdDonVi = d.Id,
                        IdNguoiDaiDien = b.Id,
                        IdLoaiHinh = o.Id,

                        IdTinhTp = d.IdTinhTp,
                        IdQuanHuyen = d.IdQuanHuyen,
                        IdXaPhuong = d.IdXaPhuong,

                        MaDonVi = d.MaDonVi,
                        TenDonVi = d.TenDonVi,
                        LstDiaChi = new List<string> { tinhTp != null ? tinhTp.Name : "", quanHuyen != null ? quanHuyen.Name : "", xaPhuong != null ? xaPhuong.Name : "", d.DiaChi },
                        MaSoThue = d.MaSoThue,
                        EmailDonVi = d.Email,
                        WebSite = d.WebSite,
                        SoDienThoaiDonVi = d.SoDienThoai,

                        TenLoaiHinh = o.TenLoaiHinh,

                        HoTen = b.HoTen,
                        ChucVu = b.ChucVu,
                        SoDienThoaiNguoiDaiDien = b.SoDienThoai,
                        EmailNguoiDaiDien = b.Email,
                        MoTa = b.MoTa,
                        Id = d.Id
                    };

        var totalCount = await query.LongCountAsync(cancellationToken: cancellationToken);
        var pageResults = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync(cancellationToken: cancellationToken);
        pageResults.ForEach(x => x.DiaChi = GetDiaChi(x.LstDiaChi));
        return new BaseQuerieResponse<DonViDto>
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Keyword = request.Keyword,
            TotalFilter = totalCount,
            Data = pageResults
        };
    }

    private static string GetDiaChi(IEnumerable<string> lstDiaChi)
    {
        return string.Join(", ", lstDiaChi.Where(x => !string.IsNullOrEmpty(x)));
    }
}

