using Microsoft.Extensions.DependencyInjection;
using System;
using SurveyApplication.Utility.HttpClientAccessorsUtils.Interfaces;

namespace SurveyApplication.Utility.HttpClientAccessorsUtils.Implementations
{
    public class BaseHttpClientFactory : IBaseHttpClientFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public BaseHttpClientFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IBaseHttpClient Create()
        {
            return _serviceProvider.GetRequiredService<IBaseHttpClient>();
        }
    }
}
