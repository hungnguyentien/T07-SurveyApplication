﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.BangKhaoSats.Handlers.Queries;

public class GetBangKhaoSatConditionsRequestHandler : BaseMasterFeatures, IRequestHandler<GetBangKhaoSatConditionsRequest, BaseQuerieResponse<BangKhaoSatDto>>
{
    private readonly IMapper _mapper;

    public GetBangKhaoSatConditionsRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseQuerieResponse<BangKhaoSatDto>> Handle(GetBangKhaoSatConditionsRequest request,
        CancellationToken cancellationToken)
    {
        var query = from d in _surveyRepo.BangKhaoSat.GetAllQueryable()
                    join b in _surveyRepo.DotKhaoSat.GetAllQueryable()
                        on d.IdDotKhaoSat equals b.Id
                    join o in _surveyRepo.LoaiHinhDonVi.GetAllQueryable()
                        on d.IdLoaiHinh equals o.Id

                    //join s in _surveyRepo.GuiEmail.GetAllQueryable()
                    //on d.Id equals s.IdBangKhaoSat
                    where (d.MaBangKhaoSat.Contains(request.Keyword) || d.TenBangKhaoSat.Contains(request.Keyword) ||
                            b.TenDotKhaoSat.Contains(request.Keyword) || o.TenLoaiHinh.Contains(request.Keyword)) &&
                            d.Deleted == false
                    select new BangKhaoSatDto
                    {
                        Id = d.Id,
                        MaBangKhaoSat = d.MaBangKhaoSat,
                        TenBangKhaoSat = d.TenBangKhaoSat,
                        MoTa = d.MoTa,
                        NgayBatDau = d.NgayBatDau,
                        NgayKetThuc = d.NgayKetThuc,
                        TrangThai = d.TrangThai,

                        IdDotKhaoSat = b.Id,
                        TenDotKhaoSat = b.TenDotKhaoSat,

                        IdLoaiHinh = o.Id,
                        TenLoaiHinh = o.TenLoaiHinh
                    };
        var totalCount = await query.LongCountAsync();
        var pageResults = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
            .ToListAsync();

        return new BaseQuerieResponse<BangKhaoSatDto>
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Keyword = request.Keyword,
            TotalFilter = totalCount,
            Data = pageResults
        };
    }
}