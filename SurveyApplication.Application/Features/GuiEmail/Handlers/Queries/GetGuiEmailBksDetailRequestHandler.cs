using MediatR;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Application.Features.GuiEmail.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.GuiEmail.Handlers.Queries
{
    public class GetGuiEmailBksDetailRequestHandler : BaseMasterFeatures, IRequestHandler<GetGuiEmailBksDetailRequest, BaseQuerieResponse<GuiEmailDto>>
    {
        public GetGuiEmailBksDetailRequestHandler(ISurveyRepositoryWrapper surveyRepository) : base(
            surveyRepository)
        {
        }

        public async Task<BaseQuerieResponse<GuiEmailDto>> Handle(GetGuiEmailBksDetailRequest request,
            CancellationToken cancellationToken)
        {
            var data = from a in _surveyRepo.GuiEmail.GetAllQueryable().AsNoTracking()
                       join b in _surveyRepo.DonVi.GetAllQueryable().AsNoTracking() on a.IdDonVi equals b.Id
                       join c in _surveyRepo.NguoiDaiDien.GetAllQueryable().AsNoTracking() on b.Id equals c.IdDonVi
                       where !a.Deleted && a.IdBangKhaoSat == request.IdBanhgKhaoSat
                                        && (request.TrangThaiGuiEmail == null || a.TrangThai == request.TrangThaiGuiEmail)
                                        && !b.Deleted
                                        && !c.Deleted
                       select new GuiEmailDto
                       {
                           DiaChiNhan = a.DiaChiNhan,
                           TenDonVi = b.TenDonVi,
                           ThoiGian = a.ThoiGian,
                           NguoiThucHien = c.HoTen,
                           TrangThai = a.TrangThai
                       };
            var totalCount = await data.LongCountAsync(cancellationToken: cancellationToken);
            var pageResults = await data.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .ToListAsync(cancellationToken: cancellationToken);
            return new BaseQuerieResponse<GuiEmailDto>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Keyword = "",
                TotalFilter = totalCount,
                Data = pageResults
            };
        }
    }
}
