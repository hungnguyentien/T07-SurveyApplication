
namespace Hangfire.Infrastructure.HttpClientAccessors.Interfaces
{
    public interface IBaseHttpClientFactory
    {
        IBaseHttpClient Create();
    }
}
