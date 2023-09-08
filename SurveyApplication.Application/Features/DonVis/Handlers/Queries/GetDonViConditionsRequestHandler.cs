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
                        //where string.IsNullOrEmpty(request.Keyword) || d.MaDonVi.Contains(request.Keyword) || d.TenDonVi.Contains(request.Keyword) ||
                        //     d.DiaChi.Contains(request.Keyword) || b.HoTen.Contains(request.Keyword)
                        select new DonViDto
                        {
                            IdLinhVuc = s.Id,
                            IdDonVi = d.Id,
                            IdNguoiDaiDien = b.Id,
                            IdLoaiHinh = o.Id,
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
            var totalCount = await query.LongCountAsync(cancellationToken: cancellationToken);
            var pageResults = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync(cancellationToken: cancellationToken);
            return new BaseQuerieResponse<DonViDto>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Keyword = request.Keyword,
                TotalCount = totalCount,
                Data = pageResults
            };
        }
    }

}
