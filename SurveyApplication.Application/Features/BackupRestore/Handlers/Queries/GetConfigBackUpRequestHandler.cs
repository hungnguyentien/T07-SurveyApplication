using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SurveyApplication.Application.DTOs.BackupRestore;
using SurveyApplication.Application.Features.BackupRestore.Requests.Queries;
using SurveyApplication.Domain.Common.Configurations;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility.LogUtils;
using static Vanara.PInvoke.Mpr;

namespace SurveyApplication.Application.Features.BackupRestore.Handlers.Queries
{
    public class GetConfigBackUpRequestHandler : BaseMasterFeatures, IRequestHandler<GetConfigBackUpRequest, ConfigJobBackupDto>
    {
        #region Properties

        private readonly ILoggerManager _logger;
        private BackupRestoreConfiguration BackupRestoreConfiguration { get; }

        #endregion

        #region Ctor

        public GetConfigBackUpRequestHandler(ISurveyRepositoryWrapper surveyRepository, ILoggerManager logger, IOptions<BackupRestoreConfiguration> backuOptions) : base(
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
            {
                _logger.LogError($"Error connecting to remote share: \n {JsonConvert.SerializeObject(result)}");
            }
        }

        #endregion

        public async Task<ConfigJobBackupDto> Handle(GetConfigBackUpRequest request, CancellationToken cancellationToken)
        {
            var rs = new ConfigJobBackupDto();
            try
            {
                var path = BackupRestoreConfiguration.DirBackupDb;
                var file = Path.Combine(path, "SetUpJob.txt");
                var datAllText = "0|0|0";
                if (File.Exists(file))
                    datAllText = await File.ReadAllTextAsync(file, cancellationToken);


                if (string.IsNullOrEmpty(datAllText)) return rs;
                for (var i = 0; i < datAllText.Split("|").Length; i++)
                {
                    var item = datAllText.Split("|")[i];
                    switch (i)
                    {
                        case 0:
                            rs.ScheduleDayofweek = Convert.ToInt32(item);
                            break;
                        case 1:
                            rs.ScheduleHour = Convert.ToInt32(item);
                            break;
                        case 2:
                            rs.ScheduleMinute = Convert.ToInt32(item);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }

            return rs;
        }
    }
}
