using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SurveyApplication.Application.Features.BackupRestore.Requests.Commands;
using SurveyApplication.Domain.Common.Configurations;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility.Constants;
using SurveyApplication.Utility.LogUtils;
using Microsoft.Extensions.Configuration;
using static Vanara.PInvoke.Mpr;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SurveyApplication.Application.Features.BackupRestore.Handlers.Commands
{
    public class ConfigBackupCommandHandler : BaseMasterFeatures,
        IRequestHandler<ConfigBackupCommand, BaseCommandResponse>
    {
        #region Properties

        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;
        private BackupRestoreConfiguration BackupRestoreConfiguration { get; }

        #endregion

        #region Ctor

        public ConfigBackupCommandHandler(ISurveyRepositoryWrapper surveyRepository, ILoggerManager logger,
            IConfiguration configuration, IOptions<BackupRestoreConfiguration> backuOptions) : base(
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

        public Task<BaseCommandResponse> Handle(ConfigBackupCommand request, CancellationToken cancellationToken)
        {
            string res;
            var path = $@"{BackupRestoreConfiguration.DirBackupDb}\";
            var nameJobs = BackupRestoreConfiguration.NamejobBackupDb;
            var cmd = CommandStep(path, BackupRestoreConfiguration.DatabaseNames.Split("|").ToList());
            var myDbConnection = _configuration.GetConnectionString(CustomString.ConnectionString);
            var timeStr = request.ConfigJobBackup.ScheduleHour.ToString("D2") + request.ConfigJobBackup.ScheduleMinute.ToString("D2") + "00";
            var startDate = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            var strartTime = int.Parse(timeStr);
            res = request.ConfigJobBackup.ScheduleDayofweek == 0 ? CreateJobsDaiLy(nameJobs, myDbConnection, cmd, strartTime, startDate) : CreateJobsForWeek(request.ConfigJobBackup.ScheduleDayofweek.ToString(), nameJobs, myDbConnection, cmd, strartTime, startDate);

            var file = Path.Combine(path, "SetUpJob.txt");
            if (!File.Exists(file))
            {
                using var sr = File.Create(file);
            }

            var textFile = $"{request.ConfigJobBackup.ScheduleDayofweek}|{request.ConfigJobBackup.ScheduleHour}|{request.ConfigJobBackup.ScheduleMinute}";
            File.WriteAllText(file, textFile);
            return Task.FromResult(new BaseCommandResponse(res));
        }

        private string CommandStep(string path, List<string> databaseNames)
        {
            string cmd = " DECLARE @name VARCHAR(50) DECLARE @nameFull VARCHAR(250) DECLARE @path VARCHAR(256) DECLARE @fileName VARCHAR(256) DECLARE @fileDate VARCHAR(20) SET @path = N''" + path
                + "'' SELECT @fileDate = replace(convert(varchar, getdate(), 101), ''/'', '''') + replace(convert(varchar, getdate(), 108), '':'', '''') DECLARE db_cursor CURSOR READ_ONLY FOR " +
                " SELECT name FROM sys.databases where name in (";
            if (databaseNames.Any())
            {
                var dbNames = new List<string>();
                foreach (var databaseName in databaseNames)
                {
                    dbNames.Add("''" + databaseName + "''");
                }

                cmd += string.Join(",", dbNames);
            }

            cmd = cmd + ") OPEN db_cursor FETCH NEXT FROM db_cursor INTO @name WHILE @@FETCH_STATUS = 0 BEGIN " +
                  " SET @fileName = @path + ''BK_'' + @name + ''_'' + @fileDate + ''.bak'' SET  @nameFull = @name + ''-Full Database Backup'' " +
                  " BACKUP DATABASE @name TO DISK = @fileName WITH NOFORMAT, NOINIT, NAME = @nameFull, SKIP, NOREWIND, NOUNLOAD, STATS = 10 " +
                  " FETCH NEXT FROM db_cursor INTO @name END CLOSE db_cursor DEALLOCATE db_cursor ";
            return cmd;
        }

        private string CreateJobsDaiLy(string nameJobs, string myDbConnection, string commandStep, int strartTime, int startDate)
        {
            var sQuery = @" USE msdb ; EXEC msdb.dbo.sp_delete_job  
            @job_name='" + nameJobs + @"' ";
            sQuery = sQuery + @" EXEC msdb.dbo.sp_add_job @job_name=N'" +
                        nameJobs + "'";

            sQuery = sQuery + @"  EXEC msdb.dbo.sp_add_jobstep 
                        @job_name='" + nameJobs + @"', 
                        @step_name=N'NameStep', 
                        @subsystem =N'TSQL',
                        @step_id=1,  ";
            sQuery = sQuery + @" @command=N'" + commandStep + "'";

            sQuery = sQuery + @" EXEC msdb.dbo.sp_add_jobschedule @job_name='" + nameJobs + @"' , @name =N'RunOnce', 
                                    @freq_type=4, @freq_interval=1, ";
            sQuery = sQuery + " @active_start_date=" + startDate + ", ";
            sQuery = sQuery + @" @active_end_date=99991231, 
                                    @active_start_time=" + strartTime + "";

            sQuery = sQuery + @" EXEC msdb.dbo.sp_add_jobserver 
            @job_name='" + nameJobs + "'";
            //Execute SQL Query
            var rowsEffected = ExecuteCommandQuery(myDbConnection, sQuery, CommandType.Text);
            return rowsEffected != -9 ? "Thiết lập thành công" : "Thiết lập thất bại";
        }

        private string CreateJobsForWeek(string dayRun, string nameJobs, string myDbConnection, string commandStep, int strartTime, int startDate)
        {
            string sQuery = @" USE msdb ; EXEC msdb.dbo.sp_delete_job  
            @job_name='" + nameJobs + @"' ";
            sQuery = sQuery + @" EXEC msdb.dbo.sp_add_job @job_name=N'" +
                        nameJobs + "'";

            sQuery = sQuery + @"  EXEC msdb.dbo.sp_add_jobstep 
                        @job_name='" + nameJobs + @"', 
                        @step_name=N'NameStep', 
                        @subsystem =N'TSQL',
                        @step_id=1,  ";
            sQuery = sQuery + @" @command=N'" + commandStep + "'";

            sQuery = sQuery + @" EXEC msdb.dbo.sp_add_jobschedule @job_name='" + nameJobs + @"' , @name =N'RunOnce', 
                                    @freq_type=8, @freq_interval=" + dayRun + @",@freq_recurrence_factor = 1, ";
            sQuery = sQuery + " @active_start_date=" + startDate + ", ";
            sQuery = sQuery + @" @active_end_date=99991231, 
                                    @active_start_time=" + strartTime + "";

            sQuery = sQuery + @" EXEC msdb.dbo.sp_add_jobserver 
            @job_name='" + nameJobs + @"'";
            //Execute SQL Query
            var rowsEffected = ExecuteCommandQuery(myDbConnection, sQuery, CommandType.Text);
            return rowsEffected != -9 ? "Thiết lập thành công" : "Thiết lập thất bại";
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
