using MediatR;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.Responses;

namespace SurveyApplication.Application.Features.CauHoi.Requests.Commands
{
    public class UpdateCauHoiCommand: IRequest<BaseCommandResponse>
    {
        public UpdateCauHoiDto CauHoiDto { get; set; }
    }
}
