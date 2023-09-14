using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Hangfire.Application.Enums;
using Hangfire.Application.Interfaces;
using Hangfire.Domain.Interfaces.Hangfire;
using Hangfire.Application.Models;
using Hangfire.Domain.Models.Hangfire;

namespace Hangfire.Application.Services
{
    public class ScheduleService : IScheduleServices
    {
        #region Prop & ctor

        private readonly IClientServices _clientServices;
        private readonly IHangfireRepositoryWrapper _hangFire;

        public ScheduleService(IHangfireRepositoryWrapper hangfire, IClientServices clientServices)
        {
            _hangFire = hangfire;
            _clientServices = clientServices;
        }

        #endregion

        public async Task<ServiceResult> UpdateOrAddJob(string jobName, string service, string apiUrl, string cronString)
        {
            var curJob = await _hangFire.JobSchedule.GetAll().FirstOrDefaultAsync(x => x.JobName == jobName);
            if (curJob == null)
            {
                var obj = new JobSchedule
                {
                    JobTypeId = (int)EnumJobType.Type.Recurring,
                    JobName = jobName,
                    Service = service,
                    ApiUrl = apiUrl,
                    CronString = cronString,
                };
                await _hangFire.JobSchedule.InsertAsync(obj);
                await _hangFire.SaveAsync();
            }
            else
            {
                curJob.JobTypeId = (int)EnumJobType.Type.Recurring;
                curJob.JobName = jobName;
                curJob.Service = service;
                curJob.ApiUrl = apiUrl;
                curJob.CronString = cronString;
                await _hangFire.JobSchedule.UpdateAsync(curJob);
                await _hangFire.SaveAsync();
            }

            RecurringJob.AddOrUpdate(jobName, () => _clientServices.RecurringJobAsync(service, apiUrl), cronString);
            return new ServiceResultSuccess("Add or Update success!!!");
        }
       

    }
}
