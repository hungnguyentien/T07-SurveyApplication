using DASHangfire.Infrastructure.Repositories;
using Hangfire.Domain.Interfaces.Hangfire;
using Hangfire.Domain.Models.Hangfire;
using Hangfire.Infrastructure.Contexts;

namespace Hangfire.Infrastructure.Repositories.Hangfire
{
    public class JobScheduleRepository : BaseRepository<JobSchedule>, IJobScheduleRepository
    {
        public JobScheduleRepository(HangfireContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
