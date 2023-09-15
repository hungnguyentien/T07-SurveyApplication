using MediatR;
using SurveyApplication.Application.Features.GuiEmail.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Application.Enums;
using SurveyApplication.Domain.Interfaces.Infrastructure;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.GuiEmail.Handlers.Commands
{
    public class ThuHoiGuiEmailCommandHandler : BaseMasterFeatures, IRequestHandler<ThuHoiGuiEmailCommand, BaseCommandResponse>
    {
        private readonly IEmailSender _emailSender;

        public ThuHoiGuiEmailCommandHandler(ISurveyRepositoryWrapper surveyRepository, IEmailSender emailSender) : base(surveyRepository)
        {
            _emailSender = emailSender;
        }

        public async Task<BaseCommandResponse> Handle(ThuHoiGuiEmailCommand request, CancellationToken cancellationToken)
        {
            var lstGuiEmail =
                (await _surveyRepo.GuiEmail.GetAllListAsync(x => !x.Deleted && request.LstIdGuiMail.Contains(x.Id))).ToList();
            if (!lstGuiEmail.Any())
                return new BaseCommandResponse
                {
                    Success = false,
                    Message = "Gửi không thành công"
                };

            var lstDiaChiNhan = request.DiaChiNhan.Split(";").Distinct().ToList();
            if (!lstDiaChiNhan.Any())
                return new BaseCommandResponse
                {
                    Success = false,
                    Message = "Gửi không thành công"
                };

            const int pageSize = 10;
            var subscriberCount = lstDiaChiNhan.Count;
            var amountOfPages = (int)Math.Ceiling((double)subscriberCount / pageSize);
            for (var pageIndex = 0; pageIndex < amountOfPages; pageIndex++)
                await RunTasks(lstDiaChiNhan.Skip(pageIndex * pageSize).Take(pageSize).ToList(), request);

            lstGuiEmail.ForEach(guiEmail =>
            {
                guiEmail.TrangThai = (int)EnumGuiEmail.TrangThai.ThuHoi;
                guiEmail.ThoiGian = DateTime.Now;
            });
            await _surveyRepo.GuiEmail.UpdateAsync(lstGuiEmail);
            await _surveyRepo.SaveAync();
            return new BaseCommandResponse
            {
                Message = "Gửi mail thu hồi thành công"
            };
        }

        private async Task RunTasks(IEnumerable<string> lstDiaChiNhan, ThuHoiGuiEmailCommand request)
        {
            var tasks = lstDiaChiNhan.Select(diaChiNhan => Task.Run(() => DoWork(diaChiNhan, request))).ToList();
            await Task.WhenAll(tasks);
        }

        private async Task DoWork(string diaChiNhan, ThuHoiGuiEmailCommand request)
        {
            var bodyEmail = $"{request.NoiDung}";
            await _emailSender.SendEmail(bodyEmail, request.TieuDe, diaChiNhan);
        }
    }
}
