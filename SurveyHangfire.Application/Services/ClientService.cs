using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Hangfire.Application.Interfaces;
using Hangfire.Infrastructure.HttpClientAccessors.Interfaces;
using SurveyApplication.Utility.LogUtils;


namespace Hangfire.Application.Services
{
    public class ClientService : IClientServices
    {
        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;
        private readonly IBaseHttpClientFactory _clientFactory;

        public ClientService(IBaseHttpClientFactory baseHttpClient, IConfiguration configuration, ILoggerManager loggerManager)
        {
            _logger = loggerManager;
            _clientFactory = baseHttpClient;
            _configuration = configuration;
        }

        public async Task<bool> RecurringJobAsync(string service, string apiUrl)
        {
            try
            {
                var client = _clientFactory.Create();
                var domain = _configuration[service];
                if (string.IsNullOrWhiteSpace(domain))
                    throw new Exception($"Not found domain {service} Service, please check appsettings config");

                var apiKey = _configuration.GetValue<string>("SecretKey");
                var response = await client.PostAsync(domain, apiUrl, accessToken: apiKey);
                _logger.LogError(response.ToString());
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return false;
            }
        }
    }
}
