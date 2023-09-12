using MediatR;
using SurveyApplication.Application.DTOs.BaoCaoCauHoi;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Commands
{
    public class CreateBaoCaoCauHoiCommand: IRequest<BaseCommandResponse>
    {
        public List<CreateBaoCaoCauHoiDto>? LstBaoCaoCauHoi { get; set; }
        public int IdGuiEmail { get; set; }
    }
}
