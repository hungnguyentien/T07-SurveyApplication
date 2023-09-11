using System.Threading.Tasks;

namespace Hangfire.Domain.Interfaces.Hangfire
{
    public interface IHangfireRepositoryWrapper
    {
        IJobScheduleRepository JobSchedule { get; }
        Task SaveAsync();
    }
}
