using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Application.Features.GuiEmail.Requests.Queries;
using SurveyApplication.Domain.Common.Configurations;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.Application.Features.GuiEmail.Handlers.Queries;

public class GetGuiEmailBksDetailRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetGuiEmailBksDetailRequest, BaseQuerieResponse<GuiEmailDto>>
{
    public GetGuiEmailBksDetailRequestHandler(ISurveyRepositoryWrapper surveyRepository,
        IOptions<EmailSettings> emailSettings) : base(surveyRepository)
    {
        EmailSettings = emailSettings.Value;
    }

    private EmailSettings EmailSettings { get; }

    public async Task<BaseQuerieResponse<GuiEmailDto>> Handle(GetGuiEmailBksDetailRequest request,
        CancellationToken cancellationToken)
    {
        var data = from a in _surveyRepo.GuiEmail.GetAllQueryable().AsNoTracking()
            join b in _surveyRepo.DonVi.GetAllQueryable().AsNoTracking() on a.IdDonVi equals b.Id
            join c in _surveyRepo.NguoiDaiDien.GetAllQueryable().AsNoTracking() on b.Id equals c.IdDonVi
            join d in _surveyRepo.KetQua.GetAllQueryable().AsNoTracking() on a.Id equals d.IdGuiEmail into joinTable
            from joinD in joinTable.DefaultIfEmpty()
            where !a.Deleted && a.IdBangKhaoSat == request.IdBanhgKhaoSat
                             && (request.TrangThaiGuiEmail == null || a.TrangThai == request.TrangThaiGuiEmail)
                             && !b.Deleted
                             && !c.Deleted
                             && a.TrangThai != (int)EnumGuiEmail.TrangThai.DangGui
            select new GuiEmailDto
            {
                DiaChiNhan = a.DiaChiNhan,
                TenDonVi = b.TenDonVi,
                ThoiGian = a.ThoiGian,
                NguoiThucHien = c.HoTen,
                TrangThai = a.TrangThai,
                IdDonVi = b.Id,
                Id = a.Id,
                LinkKhaoSat =
                    $"{EmailSettings.LinkKhaoSat}{StringUltils.EncryptWithKey(JsonConvert.SerializeObject(new EmailThongTinChungDto { IdGuiEmail = a.Id }), EmailSettings.SecretKey)}",
                IsKhaoSat = joinD != null
            };
        var totalCount = await data.LongCountAsync(cancellationToken);
        var pageResults = await data.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
            .ToListAsync(cancellationToken);
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