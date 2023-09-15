using MediatR;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.GuiEmail.Requests.Commands
{
    public class ThuHoiGuiEmailCommand : IRequest<BaseCommandResponse>
    {
        public List<int> LstIdGuiMail { get; set; }
        public string DiaChiNhan { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
    }
}
