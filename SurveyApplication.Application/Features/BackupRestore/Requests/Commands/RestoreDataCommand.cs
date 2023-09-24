using MediatR;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.BackupRestore.Requests.Commands
{
    public class RestoreDataCommand : IRequest<BaseCommandResponse>
    {
        public string[] FileNames { get; set; }
    }
}
