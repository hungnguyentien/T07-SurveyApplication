using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.DTOs.BaoCaoCauHoi;
using SurveyApplication.Application.DTOs.BaoCaoDashBoard;
using SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Queries;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.BaoCaoCauHoi.Handlers.Queries
{
    public class GetDashboardRequestHandler : BaseMasterFeatures, IRequestHandler<GetDashboardRequest, BaoCaoDashboardDto>
    {
        private readonly IMapper _mapper;
        private readonly SurveyApplicationDbContext _dbContext;

        public GetDashboardRequestHandler(ISurveyRepositoryWrapper surveyRepository, SurveyApplicationDbContext dbContext, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<BaoCaoDashboardDto> Handle(GetDashboardRequest request, CancellationToken cancellationToken)
        {
            IQueryable<DotKhaoSat> dotKhaoSatQuery = _surveyRepo.DotKhaoSat.GetAllQueryable().AsNoTracking();
            IQueryable<BangKhaoSat> bangKhaoSatQuery = _surveyRepo.BangKhaoSat.GetAllQueryable().AsNoTracking();

            if (request.NgayBatDau != null)
            {
                dotKhaoSatQuery = dotKhaoSatQuery.Where(b => b.NgayBatDau.Date >= request.NgayBatDau.Value.Date);
                bangKhaoSatQuery = bangKhaoSatQuery.Where(a => a.NgayBatDau.Date >= request. NgayBatDau.Value.Date);
            }
            if (request.NgayKetThuc != null)
            {   
                dotKhaoSatQuery = dotKhaoSatQuery.Where(b => b.NgayKetThuc.Date <= request.NgayKetThuc.Value.Date);
                bangKhaoSatQuery = bangKhaoSatQuery.Where(a => a.NgayKetThuc.Date <= request.NgayKetThuc.Value.Date);
            }
            int countDotKhaoSat = await dotKhaoSatQuery.CountAsync();
            int countBangKhaoSat = await bangKhaoSatQuery.CountAsync();

            return new BaoCaoDashboardDto
            {
                CountDotKhaoSat = countDotKhaoSat,
                CountBangKhaoSat = countBangKhaoSat
            };
        }


    }
}
