using IdentityServer4.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
            if (httpContext.Request.Path.Equals("/api/Account/login", StringComparison.OrdinalIgnoreCase) || httpContext.Request.Path.Equals("/swagger/index.html", StringComparison.OrdinalIgnoreCase) || httpContext.Request.Path.Equals("/swagger/v1/swagger.json", StringComparison.OrdinalIgnoreCase))
            {
                await _next.Invoke(httpContext);
                return;
            }

            // Lấy token từ headers
            //string token = httpContext.Request.Headers["Authorization"];

            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJoYUBnbWFpbC5jb20iLCJqdGkiOiI3MDc1MmZhMi1iOGM4LTQzNTUtOTFmOC1lYzY2M2I4NmQ1MzkiLCJlbWFpbCI6ImhhQGdtYWlsLmNvbSIsInVpZCI6IjhlNDQ1ODY1LWEyNGQtNDU0My1hNmM2LTk0NDNkMDQ4Y2RiOSIsIkFkbWluIjpbIkdldEFsbERvblZpIiwiR2V0QnlDb25kaXRpb25Eb25WaSIsIkdldEJ5RG9uVmkiLCJDcmVhdGVEb25WaSIsIlVwZGF0ZURvblZpIiwiRGVsZXRlRG9uVmkiLCJHZXRCeUNvbmRpdGlvbkxvYWlIaW5oRG9uVmkiLCJHZXRBbGxMaW5oVnVjSG9hdERvbmciLCJHZW5lcmF0ZU1hTG9haUhpbmhEb25WaSJdLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTY5NDMxODUyNCwiaXNzIjoiU3VydmV5TWFuYWdlbWVudCIsImF1ZCI6IlN1cnZleU1hbmFnZW1lbnRVc2VyIn0.Z2TbW5rrfKLQi13h4J7IPmHSIWH6uzTTKdef0jkxHS0";

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

                List<string> permissions = null;

                foreach (var role in roles)
                {
                    permissions = decodedToken.Claims.Where(c => c.Type == role).Select(c => c.Value).ToList();
                }

                if (permissions.Count == 0)
                {
                    httpContext.Response.StatusCode = 401; // Unauthorized
                    return;
                }

                List<string> path = httpContext.Request.Path.ToString().Trim('/').Split('/').ToList();

                bool hasRequiredRole = false;
                foreach (var permission in permissions)
                {
                    if (permission.Contains(path[1]) && permission.Contains(path[2]))
                    {
                        hasRequiredRole = true;
                        break;
                    }
                }

                if (!hasRequiredRole)
                {
                    httpContext.Response.StatusCode = 403; // Forbidden
                    return;
                }

                // Lưu quyền vào context để sử dụng trong các middleware khác
                httpContext.Items["permissions"] = permissions;

                await _next.Invoke(httpContext);
                return;

            }
            catch (SecurityTokenException)
            {
                httpContext.Response.StatusCode = 401; // Unauthorized
                return;
            }
        }
    }
}
