using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SurveyApplication.Application.Features.BackupRestore.Requests.Commands;
using SurveyApplication.Domain.Common.Configurations;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility.LogUtils;
using Microsoft.Extensions.Configuration;
using static Vanara.PInvoke.Mpr;
using Microsoft.Data.SqlClient;
using SurveyApplication.Utility.Constants;

namespace SurveyApplication.Application.Features.BackupRestore.Handlers.Commands
{
    internal class RestoreDataCommandHandler : BaseMasterFeatures, IRequestHandler<RestoreDataCommand, BaseCommandResponse>
    {
        #region Properties

        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;
        private BackupRestoreConfiguration BackupRestoreConfiguration { get; }

        #endregion

        #region Ctor

        public RestoreDataCommandHandler(ISurveyRepositoryWrapper surveyRepository, ILoggerManager logger, IConfiguration configuration, IOptions<BackupRestoreConfiguration> backuOptions) : base(
            surveyRepository)
        {
            _logger = logger;
            _configuration = configuration;
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

        public Task<BaseCommandResponse> Handle(RestoreDataCommand request, CancellationToken cancellationToken)
        {
            string msg;
            var error = 0;
            var success = 0;
            var myDbConnection = _configuration.GetConnectionString(CustomString.ConnectionString);
            var path = BackupRestoreConfiguration.DirBackupDb;
            if (request.FileNames.Any())
            {
                foreach (var item in request.FileNames)
                {
                    if (item == "") continue;
                    var pathRes = path + item;
                    var nameDb = item.Split('.')[0].Split('_')[1] + "_" + item.Split('.')[0].Split('_')[2];
                    var isRestoreDb = RestoreDb(pathRes, nameDb, myDbConnection);
                    if (isRestoreDb)
                        ++success;
                    else
                        ++error;
                }
            }

            if (success == 0)
                msg = $"Khôi phục không thành công {error} Database";
            else
            {
                msg = error == 0
                    ? $"Khôi phục thành công {success} Database"
                    : $"Khôi phục thành công {success} Database \n và không thành công {error} Database";
            }

            return Task.FromResult(new BaseCommandResponse(msg));
        }

        private bool RestoreDb(string fname, string databaseName, string cstr)
        {
            using var cn = new SqlConnection(cstr);
            try
            {
                cn.Open();

                #region step 1 SET SINGLE_USER WITH ROLLBACK

                var sql = "IF DB_ID('" + databaseName + "') IS NOT NULL ALTER DATABASE [" + databaseName + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
                using (var command = new SqlCommand(sql, cn))
                {
                    command.CommandTimeout = 0;
                    command.ExecuteNonQuery();
                }

                #endregion

                #region step 2 InstanceDefaultDataPath

                sql = "SELECT ServerProperty(N'InstanceDefaultDataPath') AS default_file";
                using (var command = new SqlCommand(sql, cn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                        }
                    }
                }

                sql = "SELECT ServerProperty(N'InstanceDefaultLogPath') AS default_log";
                using (var command = new SqlCommand(sql, cn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                        }
                    }
                }

                #endregion

                #region step 3 Restore

                sql = "USE MASTER RESTORE DATABASE [" + databaseName + "] FROM  DISK = N'" + fname + "' WITH  FILE = 1,  NOUNLOAD,  REPLACE,  STATS = 10";
                using (var command = new SqlCommand(sql, cn))
                {
                    command.ExecuteNonQuery();
                }

                #endregion

                #region step 4 SET MULTI_USER

                sql = "ALTER DATABASE [" + databaseName + "] SET MULTI_USER";
                using (var command = new SqlCommand(sql, cn))
                {
                    command.CommandTimeout = 0;
                    command.ExecuteNonQuery();
                }

                #endregion

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return false;
            }
        }
    }
}
