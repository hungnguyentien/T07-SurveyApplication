using MediatR;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.CauHoi.Requests.Commands
{
    public class CreateCauHoiCommand : IRequest<BaseCommandResponse>
    {
        public CreateCauHoiDto CauHoiDto { get; set; }
    }
}
