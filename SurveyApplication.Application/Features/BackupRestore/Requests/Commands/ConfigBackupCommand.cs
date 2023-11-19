using MediatR;
using SurveyApplication.Application.DTOs.BackupRestore;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.BackupRestore.Requests.Commands;

public class ConfigBackupCommand : IRequest<BaseCommandResponse>
{
    public ConfigJobBackupDto ConfigJobBackup { get; set; }
}