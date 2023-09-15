using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SurveyApplication.API.Middleware
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.Request.Path.Equals("/api/Account/login", StringComparison.OrdinalIgnoreCase) 
                || httpContext.Request.Path.Equals("/swagger/index.html", StringComparison.OrdinalIgnoreCase) 
                || httpContext.Request.Path.Equals("/swagger/v1/swagger.json", StringComparison.OrdinalIgnoreCase) 
                || httpContext.Request.Path.StartsWithSegments("/api/PhieuKhaoSat", StringComparison.OrdinalIgnoreCase))
            {
                await _next.Invoke(httpContext);
                return;
            }

            // Lấy token từ headers
            string token = httpContext.Request.Headers["Authorization"];

            // Trích xuất phần token từ chuỗi "Bearer token"
            if (token?.StartsWith("Bearer ") == true)
            {
                token = token.Substring("Bearer ".Length);
            }

            try
            {
                // Giải mã mã thông báo JWT để lấy danh sách vai trò và tên
                var handler = new JwtSecurityTokenHandler();
                var decodedToken = handler.ReadJwtToken(token);
                var roles = decodedToken.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                //TODO khác Administrator mới check quyền
                if (!roles.Exists(x => x == "Administrator"))
                {
                    var permissions = decodedToken.Claims.Where(c => roles.Contains(c.Type)).Select(c => c.Value).ToList();
                    if (permissions.Count == 0)
                    {
                        httpContext.Response.StatusCode = 401; // Unauthorized
                        return;
                    }

                    var path = httpContext.Request.Path.ToString().Trim('/').Split('/').ToList();
                    var hasRequiredRole = permissions.Any(permission => permission.Contains(path[1]) && permission.Contains(path[2]));
                    if (!hasRequiredRole)
                    {
                        httpContext.Response.StatusCode = 403; // Forbidden
                        return;
                    }

                    // Lưu quyền vào context để sử dụng trong các middleware khác
                    httpContext.Items["permissions"] = permissions;
                }

                await _next.Invoke(httpContext);
            }
            catch (SecurityTokenException)
            {
                httpContext.Response.StatusCode = 401; // Unauthorized
            }
        }
    }
}
