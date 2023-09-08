﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.DotKhaoSats.Handlers.Queries
{
   
    public class GetDotKhaoSatConditionsRequestHandler : BaseMasterFeatures, IRequestHandler<GetDotKhaoSatConditionsRequest, BaseQuerieResponse<DotKhaoSatDto>>
    {
        private readonly IMapper _mapper;
        public GetDotKhaoSatConditionsRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseQuerieResponse<DotKhaoSatDto>> Handle(GetDotKhaoSatConditionsRequest request, CancellationToken cancellationToken)
        {
            var query = from d in _surveyRepo.DotKhaoSat.GetAllQueryable()
                        join b in _surveyRepo.LoaiHinhDonVi.GetAllQueryable()
                        on d.IdLoaiHinh equals b.Id

                        where d.MaDotKhaoSat.Contains(request.Keyword) || d.TenDotKhaoSat.Contains(request.Keyword) ||
                            b.TenLoaiHinh.Contains(request.Keyword)
                        select new DotKhaoSatDto
                        {
                            Id = d.Id,
                            MaDotKhaoSat = d.MaDotKhaoSat,
                            TenDotKhaoSat = d.TenDotKhaoSat,
                            NgayBatDau = d.NgayBatDau,
                            NgayKetThuc = d.NgayKetThuc,
                            TrangThai = d.TrangThai,

                            IdLoaiHinh = b.Id,
                            TenLoaiHinh = b.TenLoaiHinh,
                            MoTa = b.MoTa,
                        };
            var totalCount = await query.LongCountAsync();
            var pageCount = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            var pageResults = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

            return new BaseQuerieResponse<DotKhaoSatDto>
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
