using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SurveyApplication.Application.Features.BackupRestore.Requests.Commands;
using SurveyApplication.Domain.Common.Configurations;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility.LogUtils;
using System.Data;
using Microsoft.Extensions.Configuration;
using static Vanara.PInvoke.Mpr;
using Microsoft.Data.SqlClient;
using SurveyApplication.Utility.Constants;

namespace SurveyApplication.Application.Features.BackupRestore.Handlers.Commands
{
    public class BackupNowCommandHandler : BaseMasterFeatures, IRequestHandler<BackupNowCommand, BaseCommandResponse>
    {
        #region Properties

        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;
        private BackupRestoreConfiguration BackupRestoreConfiguration { get; }

        #endregion

        #region Ctor

        public BackupNowCommandHandler(ISurveyRepositoryWrapper surveyRepository, ILoggerManager logger, IConfiguration configuration, IOptions<BackupRestoreConfiguration> backuOptions) : base(
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

        public Task<BaseCommandResponse> Handle(BackupNowCommand request, CancellationToken cancellationToken)
        {
            var nameJobs = BackupRestoreConfiguration.NamejobBackupDb;
            var myDbConnection = _configuration.GetConnectionString(CustomString.ConnectionString);
            var res = RunJobs(nameJobs, myDbConnection);
            return Task.FromResult(new BaseCommandResponse(res));
        }

        private string RunJobs(string nameJobs, string myDbConnection)
        {
            var sQuery = " USE msdb ; EXEC msdb.dbo.sp_start_job  N'" + nameJobs + "'; ";
            var rowsEffected = ExecuteCommandQuery(myDbConnection, sQuery, CommandType.Text);
            return rowsEffected != -9 ? "Sao lưu dữ liệu thành công" : "Sao lưu dữ liệu thất bại";
        }

        private int ExecuteCommandQuery(string myDbConnection, string sQuery, CommandType commandType)
        {
            int rowEffected;
            using var cn = new SqlConnection(myDbConnection);
            try
            {
                cn.FireInfoMessageEventOnUserErrors = true;
                cn.Open();
                var command = new SqlCommand(sQuery, cn)
                {
                    CommandType = commandType
                };
                rowEffected = command.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                rowEffected = -9;
                cn.Close();
                _logger.LogError(ex);
            }

            return rowEffected;
        }
    }
}
