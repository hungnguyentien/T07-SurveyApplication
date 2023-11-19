using System;
using System.Threading.Tasks;
using Hangfire.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using SurveyApplication.Utility.HttpClientAccessorsUtils.Interfaces;
using SurveyApplication.Utility.LogUtils;

namespace Hangfire.Application.Services;

public class ClientService : IClientServices
{
    private readonly IBaseHttpClientFactory _clientFactory;
    private readonly IConfiguration _configuration;
    private readonly ILoggerManager _logger;

    public ClientService(IBaseHttpClientFactory baseHttpClient, IConfiguration configuration,
        ILoggerManager loggerManager)
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