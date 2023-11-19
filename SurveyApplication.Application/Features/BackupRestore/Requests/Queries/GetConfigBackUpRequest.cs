using MediatR;
using SurveyApplication.Application.DTOs.BackupRestore;

namespace SurveyApplication.Application.Features.BackupRestore.Requests.Queries;

public class GetConfigBackUpRequest : IRequest<ConfigJobBackupDto>
{
}