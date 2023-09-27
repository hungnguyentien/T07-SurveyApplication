using AutoMapper;
using FluentValidation;
using MediatR;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.NguoiDaiDien;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Handlers.Queries;

public class GetThongTinChungRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetThongTinChungRequest, ThongTinChungDto>
{
    private readonly IMapper _mapper;

    public GetThongTinChungRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<ThongTinChungDto> Handle(GetThongTinChungRequest request, CancellationToken cancellationToken)
    {
        var guiEmail = await _surveyRepo.GuiEmail.GetById(request.IdGuiEmail) ??
                       throw new ValidationException("Không tìm thấy thông tin gửi mail");
        if (guiEmail.Deleted)
            throw new ValidationException("Email này không còn tồn tại");

        if (guiEmail.TrangThai == (int)EnumGuiEmail.TrangThai.ThuHoi)
            throw new ValidationException("Email này đã bị thu hồi");

        var bangKs = await _surveyRepo.BangKhaoSat.GetById(guiEmail.IdBangKhaoSat);
        switch (bangKs.TrangThai)
        {
            //TODO bảng khảo sát đã hoàn thành thì vẫn xem đc
            //case (int)EnumBangKhaoSat.TrangThai.HoanThanh:
            //    throw new ValidationException("Bảng khảo sát này đã hoàn thành");
            case (int)EnumBangKhaoSat.TrangThai.TamDung:
                throw new ValidationException("Bảng khảo sát này đang tạm dừng");
        }

        var dotKs = await _surveyRepo.DotKhaoSat.GetById(bangKs.IdDotKhaoSat);
        if(dotKs.TrangThai == (int)EnumDotKhaoSat.TrangThai.HoanThanh)
            throw new ValidationException("Đợt khảo sát này đã hoàn thành");

        var donVi = await _surveyRepo.DonVi.GetById(guiEmail.IdDonVi);
        var nguoiDaiDien =
            await _surveyRepo.NguoiDaiDien.FirstOrDefaultAsync(x => !x.Deleted && x.IdDonVi == guiEmail.IdDonVi);
        var ketQua = await _surveyRepo.KetQua.FirstOrDefaultAsync(x => x.IdGuiEmail == guiEmail.Id && !x.Deleted);
        return bangKs == null
            ? throw new ValidationException("Không tồn tại bảng khảo sát")
            : nguoiDaiDien == null
                ? throw new ValidationException("Không tồn người đại diện")
                : new ThongTinChungDto
                {
                    DonVi = _mapper.Map<DonViDto>(donVi),
                    NguoiDaiDien = _mapper.Map<NguoiDaiDienDto>(nguoiDaiDien),
                    IdGuiEmail = request.IdGuiEmail,
                    TrangThaiKq = ketQua?.TrangThai ?? 0
                };
    }
}