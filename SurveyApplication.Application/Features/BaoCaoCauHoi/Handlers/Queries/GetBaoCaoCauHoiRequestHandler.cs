using MediatR;
using SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Queries;
using AutoMapper;
using SurveyApplication.Application.DTOs.BaoCaoCauHoi;
using SurveyApplication.Domain.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Persistence;

namespace SurveyApplication.Application.Features.BaoCaoCauHoi.Handlers.Queries
{
    public class GetBaoCaoCauHoiRequestHandler : BaseMasterFeatures, IRequestHandler<GetBaoCaoCauHoiRequest, BaoCaoCauHoiDto>
    {
        private readonly IMapper _mapper;
        private readonly SurveyApplicationDbContext _dbContext;

        public GetBaoCaoCauHoiRequestHandler(ISurveyRepositoryWrapper surveyRepository, SurveyApplicationDbContext dbContext, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
             _dbContext = dbContext;
        }

        public async Task<BaoCaoCauHoiDto> Handle(GetBaoCaoCauHoiRequest request, CancellationToken cancellationToken)
        {
            //var query = from a in _surveyRepo.BangKhaoSat.GetAllQueryable().AsNoTracking()
            //            join b in _surveyRepo.DotKhaoSat.GetAllQueryable().AsNoTracking() on a.IdDotKhaoSat equals b.Id
            //            join c in _surveyRepo.GuiEmail.GetAllQueryable().AsNoTracking() on a.Id equals c.IdBangKhaoSat
            //            join d in _surveyRepo.LoaiHinhDonVi.GetAllQueryable().AsNoTracking() on c.IdDonVi equals d.Id
            //            group a by new {a} into gbks
            //            select new
            //            {

            //            };
            
            return new BaoCaoCauHoiDto
            {
                CountDonViBo = 100,
                CountDonViNganh = 20,
                CountDonViSo = 205,
                CountDonViMoi = 300,
                CountDonViTraLoi = 275,
               
            };
        }
    }
}
