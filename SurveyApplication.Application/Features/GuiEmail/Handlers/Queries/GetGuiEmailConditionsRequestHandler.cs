using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Application.Features.GuiEmails.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace SurveyApplication.Application.Features.GuiEmails.Handlers.Queries
{
   
    public class GetGuiEmailConditionsRequestHandler : BaseMasterFeatures, IRequestHandler<GetGuiEmailConditionsRequest, BaseQuerieResponse<GuiEmailDto>>
    {
        private readonly IMapper _mapper;
        public GetGuiEmailConditionsRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseQuerieResponse<GuiEmailDto>> Handle(GetGuiEmailConditionsRequest request, CancellationToken cancellationToken)
        {
            var query = from d in _surveyRepo.GuiEmail.GetAllQueryable()
                        join b in _surveyRepo.BangKhaoSat.GetAllQueryable()
                        on d.IdBangKhaoSat equals b.Id
                        where d.MaGuiEmail.Contains(request.Keyword) || b.TenBangKhaoSat.Contains(request.Keyword)
                        select new GuiEmailDto
                        {
                            Id = d.Id,
                            MaGuiEmail = d.MaGuiEmail,
                            IdBangKhaoSat = b.Id,
                            TenBangKhaoSat = b.TenBangKhaoSat,
                            DiaChiNhan = d.DiaChiNhan,
                            TieuDe = d.TieuDe,
                            NoiDung = d.NoiDung,

                            TrangThai = d.TrangThai,
                            ThoiGian = d.ThoiGian,
                        };
            var totalCount = await query.LongCountAsync();
            var pageCount = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            var pageResults = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

            return new BaseQuerieResponse<GuiEmailDto>
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
