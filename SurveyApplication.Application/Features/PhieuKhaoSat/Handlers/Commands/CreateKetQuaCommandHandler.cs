using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;
using SurveyApplication.Application.DTOs.PhieuKhaoSat.Validators;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Handlers.Commands;

public class CreateKetQuaCommandHandler : BaseMasterFeatures, IRequestHandler<CreateKetQuaCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public CreateKetQuaCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper,
        IOptions<EmailSettings> emailSettings) : base(surveyRepository)
    {
        _mapper = mapper;
        EmailSettings = emailSettings.Value;
    }

    private EmailSettings EmailSettings { get; }

    public async Task<BaseCommandResponse> Handle(CreateKetQuaCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        if (request.CreateKetQuaDto == null)
        {
            response.Success = false;
            response.Message = "Không có kết quả!";
            return response;
        }

        var validator = new CreateKetQuaDtoValidator(_surveyRepo.KetQua);
        var validationResult =
            await validator.ValidateAsync(request.CreateKetQuaDto ?? new CreateKetQuaDto(), cancellationToken);
        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = $"{(request.CreateKetQuaDto?.TrangThai == (int)EnumKetQua.TrangThai.HoanThanh ? "Gửi thông tin" : "Lưu tạm")} không thành công!";
            response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            return response;
        }

        var thongTinChung =
            JsonConvert.DeserializeObject<EmailThongTinChungDto>(
                StringUltils.DecryptWithKey(request.CreateKetQuaDto?.GuiEmail, EmailSettings.SecretKey));
        var guiEmail = await _surveyRepo.GuiEmail.GetById(thongTinChung?.IdGuiEmail ?? 0);
        if (guiEmail.Deleted)
            throw new ValidationException("Email này không còn tồn tại");

        if (guiEmail.TrangThai == (int)EnumGuiEmail.TrangThai.ThuHoi)
            throw new ValidationException("Email khảo sát này bị thu hồi");

        var bks = await _surveyRepo.BangKhaoSat.GetById(guiEmail.IdBangKhaoSat);
        switch (bks.TrangThai)
        {
            case (int)EnumBangKhaoSat.TrangThai.HoanThanh:
                throw new ValidationException("Bảng khảo sát đã hoàn thành");
            case (int)EnumBangKhaoSat.TrangThai.TamDung:
                throw new ValidationException("Bảng khảo sát đã tạm dừng");
        }

        var dotKs = await _surveyRepo.DotKhaoSat.GetById(bks.IdDotKhaoSat);
        if (dotKs.TrangThai == (int)EnumDotKhaoSat.TrangThai.HoanThanh)
            throw new ValidationException("Đợt khảo sát này đã hoàn thành");

        //var countBks =
        //    await _surveyRepo.GuiEmail.CountAsync(x => x.IdBangKhaoSat == guiEmail.IdBangKhaoSat && !x.Deleted);
        //var countKq = await _surveyRepo.KetQua.CountAsync(x =>
        //    x.IdGuiEmail == guiEmail.Id && !x.Deleted && x.TrangThai == (int)EnumKetQua.TrangThai.HoanThanh);
        //if (countBks == countKq)
        //{
        //    bks.TrangThai = (int)EnumBangKhaoSat.TrangThai.HoanThanh;
        //    await _surveyRepo.BangKhaoSat.UpdateAsync(bks);
        //}

        var ketQua = _mapper.Map<KetQua>(request.CreateKetQuaDto) ?? new KetQua();
        ketQua.IdGuiEmail = guiEmail.Id;
        if (ketQua.TrangThai == (int)EnumKetQua.TrangThai.HoanThanh)
            ketQua.DauThoiGian = DateTime.Now;

        var kqDb = await _surveyRepo.KetQua.FirstOrDefaultAsync(x =>
            request.CreateKetQuaDto != null && x.IdGuiEmail == guiEmail.Id && !x.Deleted);
        if (kqDb == null)
            await _surveyRepo.KetQua.Create(ketQua);
        else
            await _surveyRepo.KetQua.UpdateAsync(_mapper.Map(request.CreateKetQuaDto, kqDb));

        await _surveyRepo.SaveAync();
        response.Success = true;
        response.Message = $"{(request.CreateKetQuaDto?.TrangThai == (int)EnumKetQua.TrangThai.HoanThanh ? "Gửi thông tin" : "Lưu tạm")} thành công!";
        response.Id = ketQua.Id;
        return response;
    }
}