using MediatR;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.BackupRestore.Requests.Commands
{
    public class BackupNowCommand : IRequest<BaseCommandResponse>
    {
    }
}
