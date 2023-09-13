using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace SurveyApplication.API.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ValidSecretKeyAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private const string Apikeyname = "Authorization";
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(Apikeyname, out var extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api Key was not provided"
                };
                return Task.CompletedTask;
            }

            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var token = extractedApiKey.ToString();
            if (!string.IsNullOrEmpty(token))
            {
                token = token.Replace("Bearer ", "");
            }

            var apiKey = appSettings.GetValue<string>("SecretKey");
            if (apiKey.Equals(token)) return Task.CompletedTask;
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "Api Key is not valid"
            };
            return Task.CompletedTask;

        }
    }
}
