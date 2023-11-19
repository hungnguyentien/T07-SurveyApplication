using System.Threading.Tasks;
using Hangfire.Application.Models;

namespace Hangfire.Application.Interfaces;

public interface IScheduleServices
{
    Task<ServiceResult> UpdateOrAddJob(string jobName, string service, string apiUrl, string cronString);
}