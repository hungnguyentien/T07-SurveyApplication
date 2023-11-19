using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SurveyApplication.Application.DTOs.BackupRestore;
using SurveyApplication.Application.Features.BackupRestore.Requests.Queries;
using SurveyApplication.Domain.Common.Configurations;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility;
using SurveyApplication.Utility.LogUtils;
using static Vanara.PInvoke.Mpr;

namespace SurveyApplication.Application.Features.BackupRestore.Handlers.Queries;

public class GetBackupRestoreConditionsRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetBackupRestoreConditionsRequest, BaseQuerieResponse<BackupRestoreDto>>
{
    #region Ctor

    public GetBackupRestoreConditionsRequestHandler(ISurveyRepositoryWrapper surveyRepository, ILoggerManager logger,
        IOptions<BackupRestoreConfiguration> backuOptions) : base(
        surveyRepository)
    {
        _logger = logger;
        BackupRestoreConfiguration = backuOptions.Value;
        if (string.IsNullOrEmpty(BackupRestoreConfiguration.DirBackupDb)) return;
        var userName = BackupRestoreConfiguration.UserName;
        var pwd = BackupRestoreConfiguration.Password;
        var remoteName = BackupRestoreConfiguration.DirBackupDb;
        var netResource = new NETRESOURCE(remoteName);
        var result = WNetAddConnection2(netResource, pwd, userName);
        if (result != 0)
            _logger.LogError($"Error connecting to remote share: \n {JsonConvert.SerializeObject(result)}");
    }

    #endregion

    public Task<BaseQuerieResponse<BackupRestoreDto>> Handle(GetBackupRestoreConditionsRequest request,
        CancellationToken cancellationToken)
    {
        var data = new List<BackupRestoreDto>();
        try
        {
            var path = $"{BackupRestoreConfiguration.DirBackupDb}\\";
            var files = Directory.GetFiles(path, "*.bak");
            for (var i = 0; i < files.ToList().Count; i++)
            {
                var fileName = files[i];
                var fileInfo = new FileInfo(fileName);
                data.Add(new BackupRestoreDto
                {
                    Id = i,
                    Name = fileInfo.Name,
                    CreateDateTime = fileInfo.CreationTime,
                    TimeBackup = fileInfo.CreationTime.ToString("dd/MM/yyyy HH:mm:ss"),
                    Capacity = Ultils.ConvertCapacity(fileInfo.Length)
                });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex);
        }

        data = data.Where(x => string.IsNullOrEmpty(request.Keyword) || x.Name.Contains(request.Keyword)).ToList();
        var totalFilter = data.Count;
        var result = data.OrderByDescending(x => x.CreateDateTime).Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize).ToList();
        return Task.FromResult(new BaseQuerieResponse<BackupRestoreDto>
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Keyword = request.Keyword,
            TotalFilter = totalFilter,
            Data = result
        });
    }

    #region Properties

    private readonly ILoggerManager _logger;
    private BackupRestoreConfiguration BackupRestoreConfiguration { get; }

    #endregion
}