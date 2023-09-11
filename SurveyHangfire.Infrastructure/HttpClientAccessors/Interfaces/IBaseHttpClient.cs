using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hangfire.Infrastructure.HttpClientAccessors.Interfaces
{
    public interface IBaseHttpClient
    {
        Task<bool> PostAsync(string domain, string apiEndpoint, object data = null,
             Dictionary<string, string> requestParams = null,
             Dictionary<string, string> headers = null,
             string accessToken = "", AccessTokenType accessTokenType = AccessTokenType.Bearer);
    }
}
