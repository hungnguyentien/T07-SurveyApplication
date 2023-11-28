using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.DTOs.GuiEmail.Validators;
using SurveyApplication.Application.Features.GuiEmail.Requests.Commands;
using SurveyApplication.Domain.Common.Configurations;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Infrastructure;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.Application.Features.GuiEmail.Handlers.Commands
{
    public class GuiLaiGuiEmailCommandHandler : BaseMasterFeatures, IRequestHandler<GuiLaiGuiEmailCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private EmailSettings EmailSettings { get; }

        public GuiLaiGuiEmailCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper, IEmailSender emailSender,
            IOptions<EmailSettings> emailSettings) : base(surveyRepository)
        {
            _mapper = mapper;
            _emailSender = emailSender;
            EmailSettings = emailSettings.Value;
        }

        public async Task<BaseCommandResponse> Handle(GuiLaiGuiEmailCommand request, CancellationToken cancellationToken)
        {
            var lstGuiEmail =
                (await _surveyRepo.GuiEmail.GetAllListAsync(x => !x.Deleted && request.LstIdGuiMail.Contains(x.Id) && x.TrangThai == (int)EnumGuiEmail.TrangThai.GuiLoi)).ToList();

            var lstGuiEmailThuHoiOld =
                (await _surveyRepo.GuiEmail.GetAllListAsync(x => !x.Deleted && request.LstIdGuiMail.Contains(x.Id) && x.TrangThai == (int)EnumGuiEmail.TrangThai.ThuHoi)).ToList();

            #region Kiểm tra và thêm email thu hồi

            if (lstGuiEmailThuHoiOld.Any())
            {
                var dateNow = DateTime.Now.Date;
                var response = new BaseCommandResponse();
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
                    throw new ValidationException("Bảng khảo sát chưa đến hạn hoặc đã hết hạn");

                var lstBangKhaoSat = (await _surveyRepo.BangKhaoSat.GetAllListAsync(x =>
                    !x.Deleted && x.NgayBatDau.Date <= dateNow && x.NgayKetThuc.Date >= dateNow &&
                    request.GuiEmailDto.LstBangKhaoSat.Contains(x.Id))).ToList();
                if (!lstBangKhaoSat.Any())
                    throw new ValidationException("Không tìm thấy bảng khảo sát");

                if (await _surveyRepo.DotKhaoSat.AnyAsync(x =>
                        !x.Deleted && lstBangKhaoSat.Select(b => b.IdDotKhaoSat).Contains(x.Id) &&
                        (x.NgayBatDau.Date > dateNow || x.NgayKetThuc.Date < dateNow)))
                    throw new ValidationException("Đợt khảo sát chưa đến hạn hoặc đã hết hạn");

                var lstDonVi = (await _surveyRepo.DonVi.GetAllListAsync(x => request.GuiEmailDto.LstIdDonVi.Contains(x.Id) && !x.Deleted)).ToList();
                if (!lstDonVi.Any())
                    throw new ValidationException("Không tìm thấy đơn vị");
                var lstGuiEmailThuHoi = new List<Domain.GuiEmail>();
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
                        lstGuiEmailThuHoi.Add(guiEmail);
                    }

                await _surveyRepo.GuiEmail.InsertAsync(lstGuiEmailThuHoi);
                await _surveyRepo.SaveAync();
                if (lstGuiEmailThuHoi.Any())
                    lstGuiEmail.AddRange(lstGuiEmailThuHoi);
            }

            #endregion

            #region Gửi email

            const int pageSize = 10;
            var subscriberCount = lstGuiEmail.Count();
            var amountOfPages = (int)Math.Ceiling((double)subscriberCount / pageSize);
            for (var pageIndex = 0; pageIndex < amountOfPages; pageIndex++)
                await RunTasks(lstGuiEmail.Skip(pageIndex * pageSize).Take(pageSize).ToList());

            #endregion

            //TODO xóa email thu hồi cũ
            if (!lstGuiEmailThuHoiOld.Any())
                return new BaseCommandResponse
                {
                    Message = "Gửi mail thu hồi thành công"
                };
            {
                lstGuiEmailThuHoiOld.ForEach(guiEmail =>
                {
                    guiEmail.Deleted = true;
                });
                await _surveyRepo.GuiEmail.UpdateAsync(lstGuiEmailThuHoiOld);
                await _surveyRepo.SaveAync();
            }

            return new BaseCommandResponse
            {
                Message = "Gửi mail thu hồi thành công"
            };
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
}
