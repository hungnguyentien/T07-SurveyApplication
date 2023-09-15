using System.Threading.Tasks;
using Hangfire.Application.Enums;
using Hangfire.Application.Interfaces;
using Hangfire.Application.Models;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace Hangfire.Application.Services
{
    public class ScheduleService : BaseMasterService, IScheduleServices
    {
        #region Prop & ctor

        private readonly IClientServices _clientServices;

        public ScheduleService(ISurveyRepositoryWrapper surveyRepository, IClientServices clientServices) : base(surveyRepository)
        {
            _clientServices = clientServices;
        }

        #endregion

        public async Task<ServiceResult> UpdateOrAddJob(string jobName, string service, string apiUrl, string cronString)
        {
            var curJob = await _surveyRepo.JobSchedule.GetAllQueryable().FirstOrDefaultAsync(x => x.JobName == jobName);
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
                await _surveyRepo.JobSchedule.InsertAsync(obj);
                await _surveyRepo.SaveAync();
            }
            else
            {
                curJob.JobTypeId = (int)EnumJobType.Type.Recurring;
                curJob.JobName = jobName;
                curJob.Service = service;
                curJob.ApiUrl = apiUrl;
                curJob.CronString = cronString;
                await _surveyRepo.JobSchedule.UpdateAsync(curJob);
                await _surveyRepo.SaveAync();
            }

            RecurringJob.AddOrUpdate(jobName, () => _clientServices.RecurringJobAsync(service, apiUrl), cronString);
            return new ServiceResultSuccess("Add or Update success!!!");
        }
       

    }
}
