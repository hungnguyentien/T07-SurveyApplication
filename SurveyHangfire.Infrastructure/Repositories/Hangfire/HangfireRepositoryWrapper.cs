using System.Threading.Tasks;
using Hangfire.Domain.Interfaces.Hangfire;
using Hangfire.Infrastructure.Contexts;

namespace Hangfire.Infrastructure.Repositories.Hangfire
{
    public class HangfireRepositoryWrapper : IHangfireRepositoryWrapper
    {
        #region ctor

        private readonly HangfireContext _repoContext;
        public HangfireRepositoryWrapper(HangfireContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }


        #endregion ctor

        private IJobScheduleRepository _jobSchedule;
        public IJobScheduleRepository JobSchedule => _jobSchedule ??= new JobScheduleRepository(_repoContext);

        public async Task SaveAsync()
        {
            await _repoContext.SaveChangesAsync();
        }
    }
}
