using System.Threading.Tasks;

namespace Hangfire.Application.Interfaces
{
    public interface IClientServices
    {
        Task<bool> RecurringJobAsync(string service, string apiUrl);
    }
}
