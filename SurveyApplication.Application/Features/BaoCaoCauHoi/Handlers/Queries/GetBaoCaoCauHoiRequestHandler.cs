using MediatR;
using SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Queries;
using AutoMapper;
using SurveyApplication.Application.DTOs.BaoCaoCauHoi;
using SurveyApplication.Domain.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Domain;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.BaoCaoCauHoi.Handlers.Queries
{
    public class GetBaoCaoCauHoiRequestHandler : BaseMasterFeatures, IRequestHandler<GetBaoCaoCauHoiRequest, BaoCaoCauHoiDto>
    {
        private readonly IMapper _mapper;

        public GetBaoCaoCauHoiRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaoCaoCauHoiDto> Handle(GetBaoCaoCauHoiRequest request, CancellationToken cancellationToken)
        {
            var query = (from a in _surveyRepo.BaoCaoCauHoi.GetAllQueryable()
                        //join b in _surveyRepo.BangKhaoSat.GetAllQueryable() on a.IdBangKhaoSat equals b.Id
                        //join c in _surveyRepo.DotKhaoSat.GetAllQueryable() on a.IdDotKhaoSat equals c.Id
                        //join d in _surveyRepo.CauHoi.GetAllQueryable() on a.IdCauHoi equals d.Id
                        //join e in _surveyRepo.LoaiHinhDonVi.GetAllQueryable() on a.IdLoaiHinhDonVi equals e.Id

                        //where (b.Id == request.IdBangKhaoSat || c.Id == request.IdDotKhaoSat ||
                        //     d.Id == request.IdLoaiHinhDonVi) && a.Deleted == false

                        select new BaoCaoCauHoiDto
                        {
                            //IdBangKhaoSat = b.Id,
                            //IdDotKhaoSat = c.Id,
                            //IdCauHoi = d.Id,
                            //IdLoaiHinhDonVi = e.Id,

                            IdBangKhaoSat = a.IdBangKhaoSat,
                            IdDotKhaoSat = a.IdDotKhaoSat,
                            IdCauHoi = a.IdCauHoi,
                            IdLoaiHinhDonVi = a.IdLoaiHinhDonVi,

                            CountDonViBo = 100,
                            CountDonViNganh = 20,
                            CountDonViSo = 205,
                            CountDonViMoi = 300,
                            CountDonViTraLoi = 275
                        }).ToList();

            var totalCount = query.LongCount();

            var pageResults = query.GroupBy(g => g.IdDotKhaoSat).OrderBy(o => o.Key).ToList(); ;

            //return new BaseQuerieResponse<BaoCaoCauHoiDto>
            //{
            //    PageIndex = request.PageIndex,
            //    PageSize = request.PageSize,
            //    Keyword = request.Keyword,
            //    TotalFilter = totalCount,
            //    Data = pageResults
            //};

            return new BaoCaoCauHoiDto
            {
                CountDonViBo = 100,
                CountDonViNganh = 20,
                CountDonViSo = 205,
                CountDonViMoi = 300,
                CountDonViTraLoi = 275
            };
        }
    }
}
