using MediatR;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.CauHoi.Requests.Commands
{
    public class UpdateCauHoiCommand: IRequest<BaseCommandResponse>
    {
        public UpdateCauHoiDto CauHoiDto { get; set; }
    }
}
