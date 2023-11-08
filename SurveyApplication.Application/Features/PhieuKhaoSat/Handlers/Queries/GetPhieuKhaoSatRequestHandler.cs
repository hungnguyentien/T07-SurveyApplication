using MediatR;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SurveyApplication.Domain.Common.Configurations;
using SurveyApplication.Utility;
using Microsoft.Extensions.Options;
using SurveyApplication.Application.DTOs.CauHoi;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Handlers.Queries
{
    public class GetPhieuKhaoSatRequestHandler : BaseMasterFeatures, IRequestHandler<GetPhieuKhaoSatRequest, BaseQuerieResponse<PhieuKhaoSatDoanhNghiepDto>>
    {
        private EmailSettings EmailSettings { get; }
        public GetPhieuKhaoSatRequestHandler(ISurveyRepositoryWrapper surveyRepository, IOptions<EmailSettings> emailSettings) : base(surveyRepository)
        {
            EmailSettings = emailSettings.Value;
        }

        public async Task<BaseQuerieResponse<PhieuKhaoSatDoanhNghiepDto>> Handle(GetPhieuKhaoSatRequest request, CancellationToken cancellationToken)
        {
            var query = from dv in _surveyRepo.DonVi.GetAllQueryable()
                        join gm in _surveyRepo.GuiEmail.GetAllQueryable() on dv.Id equals gm.IdDonVi
                        join bks in _surveyRepo.BangKhaoSat.GetAllQueryable() on gm.IdBangKhaoSat equals bks.Id
                        where !dv.Deleted && (dv.MaDonVi == request.Keyword || dv.MaSoThue == request.Keyword)
                            && !gm.Deleted && !bks.Deleted
                        orderby gm.Created descending
                        select new PhieuKhaoSatDoanhNghiepDto
                        {
                            DiaChiNhan = gm.DiaChiNhan,
                            LinkKhaoSat = StringUltils.EncryptWithKey(JsonConvert.SerializeObject(new EmailThongTinChungDto
                            {
                                IdGuiEmail = gm.Id
                            }), EmailSettings.SecretKey),
                            TieuDe = gm.TieuDe,
                            TenBangKhaoSat = bks.TenBangKhaoSat,
                            NgayBatDau = bks.NgayBatDau,
                            NgayKetThuc = bks.NgayKetThuc
                        };
            var result = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .ToListAsync(cancellationToken: cancellationToken);
            return new BaseQuerieResponse<PhieuKhaoSatDoanhNghiepDto>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Keyword = request.Keyword,
                TotalFilter = result.LongCount(),
                Data = result
            };
        }
    }
}
