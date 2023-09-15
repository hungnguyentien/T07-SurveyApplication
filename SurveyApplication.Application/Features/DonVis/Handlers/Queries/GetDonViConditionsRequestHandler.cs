using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.Features.DonVis.Requests.Queries;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.DonVis.Handlers.Queries
{
   
    public class GetDonViConditionsRequestHandler : BaseMasterFeatures, IRequestHandler<GetDonViConditionsRequest, BaseQuerieResponse<DonViDto>>
    {
        private readonly IMapper _mapper;
        public GetDonViConditionsRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
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
                        on d.IdLinhVuc equals s.Id

                        //join x in _surveyRepo.TinhTp.GetAllQueryable()
                        //on d.IdTinhTp equals x.Id
                        //join y in _surveyRepo.QuanHuyen.GetAllQueryable()
                        //on d.IdQuanHuyen equals y.Id
                        //join z in _surveyRepo.XaPhuong.GetAllQueryable()
                        //on d.IdXaPhuong equals z.Id

                        where (d.MaDonVi.Contains(request.Keyword) || d.TenDonVi.Contains(request.Keyword) ||
                             d.DiaChi.Contains(request.Keyword) || b.HoTen.Contains(request.Keyword)) &&
                             d.Deleted == false
                        select new DonViDto
                        {
                            IdLinhVuc = s.Id,
                            IdDonVi = d.Id,
                            IdNguoiDaiDien = b.Id,
                            IdLoaiHinh = o.Id,

                            //IdTinhTp = x.Id,
                            //IdQuanHuyen = y.Id,
                            //IdXaPhuong= z.Id,

                            MaDonVi = d.MaDonVi,
                            TenDonVi = d.TenDonVi,
                            DiaChi = d.DiaChi,
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
                        };
            var totalCount = await query.LongCountAsync();
            var pageCount = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            var pageResults = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
            
            return new BaseQuerieResponse<DonViDto>
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
