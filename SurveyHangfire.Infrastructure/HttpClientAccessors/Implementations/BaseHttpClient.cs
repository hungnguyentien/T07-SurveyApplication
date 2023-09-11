using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Hangfire.Infrastructure.HttpClientAccessors.Interfaces;

namespace Hangfire.Infrastructure.HttpClientAccessors.Implementations
{
    public class BaseHttpClient : IBaseHttpClient

    {
        private readonly HttpClient _httpClient;
        public BaseHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        #region Post

        public async Task<bool> PostAsync(string domain, string apiEndpoint, object data = null, Dictionary<string, string> requestParams = null,
            Dictionary<string, string> headers = null,
            string accessToken = "", AccessTokenType accessTokenType = AccessTokenType.Bearer)
        {
            var requestUri = $"{domain}";
            if (!string.IsNullOrWhiteSpace(apiEndpoint))
            {
                requestUri += $"/{apiEndpoint}";
            }
            requestUri = PopulateRequestParam(requestParams, requestUri); //Thêm param
            var message = new HttpRequestMessage(HttpMethod.Post, requestUri);
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var dataStr = string.Empty;
            if (data != null)
            {
                dataStr = !(data is string) ? JsonConvert.SerializeObject(data) : data.ToString();
            }

            AppendAccessToken(message, accessToken, accessTokenType);
            AddRequestHeader(message, headers);
            AppendHttpContent(message, dataStr);
            try
            {
                var response = await _httpClient.SendAsync(message).ConfigureAwait(false);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        #endregion Post

        #region Private

        /// <summary>
        /// Thêm param vào URI
        /// </summary>
        /// <param name="requestParams"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        private static string PopulateRequestParam(Dictionary<string, string> requestParams, string requestUri)
        {
            if (requestParams != null && requestParams.Count > 0)
            {
                requestUri += "?";

                foreach (var item in requestParams)
                {
                    requestUri += $"{item.Key}={item.Value}&";
                }

                if (requestUri.EndsWith("&"))
                {
                    requestUri = requestUri.Substring(0, requestUri.Length - 1);
                }
            }

            return requestUri;
        }

        private static void AddRequestHeader(HttpRequestMessage requestMessage, Dictionary<string, string> headers)
        {
            //client.Timeout = TimeSpan.FromSeconds(GetRequestTimeout());
            if (requestMessage != null && headers != null)
            {
                foreach (var item in headers)
                {
                    requestMessage.Headers.Add(item.Key, item.Value);
                }
            }
        }

        private static void AppendHttpContent(HttpRequestMessage requestMessage, string httpContentStr)
        {
            if (requestMessage != null && !string.IsNullOrWhiteSpace(httpContentStr))
            {
                requestMessage.Content = new StringContent(httpContentStr, Encoding.UTF8, "application/json");
            }
        }

        /// <summary>
        /// Append Access Token vào HttpRequest
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="accessToken"></param>
        /// <param name="accessTokenType"></param>
        private static void AppendAccessToken(HttpRequestMessage requestMessage, string accessToken, AccessTokenType accessTokenType)
        {
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                if (accessTokenType == AccessTokenType.Bearer)
                {
                    //client.SetBearerToken(accessToken);
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }
                else if (accessTokenType == AccessTokenType.Basic)
                {
                    var arr = accessToken.Split("@#$");
                    var authenticationString = $"{arr[0]}:{arr[1]}";
                    var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
                }
                else
                {
                    requestMessage.Headers.TryAddWithoutValidation("Authorization", accessToken);
                }
            }
        }

        #endregion Private
    }
}
