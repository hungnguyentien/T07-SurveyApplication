using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.Enums;
using SurveyApplication.Application.Features.CauHoi.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.CauHoi.Handlers.Queries;

public class GetConfigCauHoiRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetConfigCauHoiRequest, PhieuKhaoSatDto>
{
    private readonly IMapper _mapper;

    public GetConfigCauHoiRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<PhieuKhaoSatDto> Handle(GetConfigCauHoiRequest request, CancellationToken cancellationToken)
    {
        var mailInfo = await _surveyRepo.GuiEmail.GetById(request.IdGuiEmail) ??
                       throw new ValidationException("Không tìm thấy thông tin gửi mail");
        var bks = await _surveyRepo.BangKhaoSat.GetById(mailInfo.IdBangKhaoSat) ??
                  throw new ValidationException("Không tồn tại bảng khảo sát");
        var query = from a in _surveyRepo.BangKhaoSatCauHoi.GetAllQueryable()
            join b in _surveyRepo.CauHoi.GetAllQueryable() on a.IdCauHoi equals b.Id
            where a.IdBangKhaoSat == bks.Id && b.ActiveFlag == (int)EnumCommon.ActiveFlag.Active && !a.Deleted &&
                  !b.Deleted
            orderby a.Priority
            select new CauHoiDto
            {
                Id = b.Id,
                ActiveFlag = b.ActiveFlag,
                BatBuoc = a.IsRequired,
                IsOther = b.IsOther,
                KichThuocFile = b.KichThuocFile,
                LabelCauTraLoi = b.LabelCauTraLoi,
                LoaiCauHoi = b.LoaiCauHoi,
                MaCauHoi = b.MaCauHoi,
                TieuDe = b.TieuDe
            };
        var lstCauHoi = await query.ToListAsync(cancellationToken);
        var lstId = lstCauHoi.Select(x => x.Id ?? 0).ToList();
        var lstCot = await _surveyRepo.Cot.GetAllListAsync(x => lstId.Contains(x.IdCauHoi));
        var lstHang = await _surveyRepo.Hang.GetAllListAsync(x => lstId.Contains(x.IdCauHoi));
        var kq = await _surveyRepo.KetQua.FirstOrDefaultAsync(x => x.IdGuiEmail == request.IdGuiEmail && !x.Deleted);
        lstCauHoi.ForEach(x =>
        {
            x.LstCot = _mapper.Map<List<CotDto>>(lstCot.Where(c => c.IdCauHoi == x.Id).Select(c => c));
            x.LstHang = _mapper.Map<List<HangDto>>(lstHang.Where(h => h.IdCauHoi == x.Id).Select(h => h));
        });
        var result = new PhieuKhaoSatDto
        {
            IdBangKhaoSat = mailInfo.IdBangKhaoSat,
            TrangThaiKhaoSat = bks.TrangThai ?? (int)EnumBangKhaoSat.TrangThai.ChoKhaoSat,
            LstCauHoi = lstCauHoi,
            KqSurvey = kq?.Data ?? "",
            TrangThaiKq = kq?.TrangThai ?? (int)EnumKetQua.TrangThai.ChuaLuu
        };

        return result;
    }
}