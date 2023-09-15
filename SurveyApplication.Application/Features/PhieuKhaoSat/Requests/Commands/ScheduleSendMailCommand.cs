using MediatR;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands
{
    public class ScheduleSendMailCommand : IRequest<BaseCommandResponse>
    {
    }
}
