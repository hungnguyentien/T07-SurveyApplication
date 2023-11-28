using MediatR;
using SurveyApplication.Application.DTOs.BackupRestore;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.BackupRestore.Requests.Queries
{
    public class GetBackupRestoreConditionsRequest : IRequest<BaseQuerieResponse<BackupRestoreDto>>
    {
        public List<BackupRestoreDto> Data { get; set; }
        public int PageCount { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string? Keyword { get; set; }
    }
}
