using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.DTOs.GuiEmail.Validators;
using SurveyApplication.Application.Enums;
using SurveyApplication.Application.Features.GuiEmail.Requests.Commands;
using SurveyApplication.Domain.Common;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Infrastructure;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility;

namespace SurveyApplication.Application.Features.GuiEmail.Handlers.Commands;

public class SendKhaoSatCommandHandler : BaseMasterFeatures, IRequestHandler<SendKhaoSatCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private EmailSettings EmailSettings { get; }

    public SendKhaoSatCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper, IEmailSender emailSender,
        IOptions<EmailSettings> emailSettings) : base(surveyRepository)
    {
        _mapper = mapper;
        _emailSender = emailSender;
        EmailSettings = emailSettings.Value;
    }

    public async Task<BaseCommandResponse> Handle(SendKhaoSatCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();

        #region Thêm mới gửi email

        var dateNow = DateTime.Now.Date;
        if (request.GuiEmailDto == null)
            throw new ValidationException("Gửi email không được để trống");

        var validator = new CreateGuiEmailDtoValidator(_surveyRepo.GuiEmail);
        var validatorResult = await validator.ValidateAsync(request.GuiEmailDto, cancellationToken);
        if (validatorResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Tạo mới thất bại";
            response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
            return response;
        }

        if (await _surveyRepo.BangKhaoSat.AnyAsync(x =>
                !x.Deleted && request.GuiEmailDto.LstBangKhaoSat.Contains(x.Id) &&
                (x.NgayBatDau.Date > dateNow || x.NgayKetThuc.Date < dateNow)))
            throw new ValidationException("Có bảng khảo sát chưa đến hạn hoặc đã hết hạn");

        var lstBangKhaoSat = await _surveyRepo.BangKhaoSat.GetAllListAsync(x =>
                                 !x.Deleted && x.NgayBatDau.Date <= dateNow && x.NgayKetThuc.Date >= dateNow &&
                                 request.GuiEmailDto.LstBangKhaoSat.Contains(x.Id)) ??
                             throw new ValidationException("Bảng khảo sát đã hết hạn");
        if (!lstBangKhaoSat.Any())
            throw new ValidationException("Không tìm thấy bảng khảo sát");

        if (await _surveyRepo.DotKhaoSat.AnyAsync(x =>
                !x.Deleted && lstBangKhaoSat.Select(b => b.IdDotKhaoSat).Contains(x.Id) &&
                (x.NgayBatDau.Date > dateNow || x.NgayKetThuc.Date < dateNow)))
            throw new ValidationException("Có đợt khảo sát chưa đến hạn hoặc đã hết hạn");

        var lstDonVi =
            await _surveyRepo.DonVi.GetAllListAsync(x => request.GuiEmailDto.LstIdDonVi.Contains(x.Id) && !x.Deleted) ??
            throw new ValidationException("Không tìm thấy đơn vị");

        if (!lstDonVi.Any())
            throw new ValidationException("Không tìm thấy đơn vị");
        var lstGuiEmail = new List<Domain.GuiEmail>();
        foreach (var bangKhaoSat in lstBangKhaoSat)
            foreach (var donVi in lstDonVi)
            {
                var guiEmail = _mapper.Map<Domain.GuiEmail>(request.GuiEmailDto) ??
                               throw new ValidationException("Gửi email không mapping được");
                guiEmail.IdBangKhaoSat = bangKhaoSat.Id;
                guiEmail.IdDonVi = donVi.Id;
                guiEmail.MaGuiEmail = Guid.NewGuid().ToString();
                guiEmail.DiaChiNhan = donVi.Email;
                guiEmail.TrangThai = (int)EnumGuiEmail.TrangThai.DangGui;
                guiEmail.ThoiGian = DateTime.Now;
                lstGuiEmail.Add(guiEmail);
            }

        await _surveyRepo.GuiEmail.InsertAsync(lstGuiEmail);
        await _surveyRepo.SaveAync();

        #endregion

        //#region Gửi email

        //const int pageSize = 10;
        //var subscriberCount = lstGuiEmail.Count();
        //var amountOfPages = (int)Math.Ceiling((double)subscriberCount / pageSize);
        //for (var pageIndex = 0; pageIndex < amountOfPages; pageIndex++)
        //    await RunTasks(lstGuiEmail.Skip(pageIndex * pageSize).Take(pageSize).ToList());

        //#endregion

        response.Success = true;
        response.Message = "Gửi email thành công";
        return response;
    }

    private async Task RunTasks(IEnumerable<Domain.GuiEmail> lstGuiEmail)
    {
        var tasks = lstGuiEmail.Select(guiEmail => Task.Run(() => DoWork(guiEmail))).ToList();
        await Task.WhenAll(tasks);
    }

    private async Task DoWork(Domain.GuiEmail guiEmail)
    {
        var thongTinChung = new EmailThongTinChungDto
        {
            IdGuiEmail = guiEmail.Id
        };
        var linkKhaoSat = $"\n {EmailSettings.LinkKhaoSat}{StringUltils.EncryptWithKey(JsonConvert.SerializeObject(thongTinChung), EmailSettings.SecretKey)}";
        var bodyEmail = $"{guiEmail.NoiDung} {linkKhaoSat}";
        var resultSend = await _emailSender.SendEmail(bodyEmail, guiEmail.TieuDe, guiEmail.DiaChiNhan);
        guiEmail.TrangThai = resultSend.IsSuccess
            ? (int)EnumGuiEmail.TrangThai.ThanhCong
            : (int)EnumGuiEmail.TrangThai.GuiLoi;
        guiEmail.ThoiGian = DateTime.Now;
        await _surveyRepo.GuiEmail.UpdateAsync(guiEmail);
        await _surveyRepo.SaveAync();
    }
}