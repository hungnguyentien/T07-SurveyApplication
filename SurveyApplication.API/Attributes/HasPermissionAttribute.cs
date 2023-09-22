using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SurveyApplication.Domain;
using System.Security.Claims;

namespace SurveyApplication.API.Attributes
{
    public class HasPermissionAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        /// <summary>
        /// Permission string to get from controller
        /// </summary>
        private readonly int[] _permissions;
        /// <summary>
        /// Code modules string to get from controller
        /// </summary>
        private readonly int[] _codeModules;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="codeModules"></param>
        /// <param name="permission"></param>
        public HasPermissionAttribute(int[] codeModules, int[] permission)
        {
            _codeModules = codeModules;
            _permissions = permission;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                // Lấy token từ headers
                var token = GetToken(context);
                if (string.IsNullOrEmpty(token))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                var handler = new JwtSecurityTokenHandler();
                var decodedToken = handler.ReadJwtToken(token);

                // Kiểm tra xem vai trò có bị đánh dấu Deleted không
                var roleManager = context.HttpContext.RequestServices.GetRequiredService<RoleManager<Role>>();
                var rolesInToken = decodedToken.Claims.Where(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Select(x => x.Value);
                foreach (var roleName in rolesInToken)
                {
                    var role = await roleManager.FindByNameAsync(roleName);
                    if (role is not { Deleted: true }) continue;
                    context.Result = new ForbidResult(); // Forbidden
                    return;
                }
                // Tiếp tục kiểm tra quyền truy cập bình thường
                var isAccess = decodedToken.Claims.Any(x => _codeModules.Select(m => m.ToString()).Contains(x.Type) && _permissions.All(p => JsonExtensions.DeserializeFromJson<List<int>>(x.Value).Contains(p)));
                if (!isAccess)
                    context.Result = new ForbidResult(); // Forbidden
            }
            catch (SecurityTokenException)
            {
                context.Result = new UnauthorizedResult(); // Unauthorized
            }
        }

        #region Private

        private string? GetToken(AuthorizationFilterContext context)
        {
            var token = string.Empty;
            var auth = context.HttpContext.Request.Headers["Authorization"].ToString();
            token = string.IsNullOrWhiteSpace(auth) switch
            {
                false when auth != null && auth.ToLower().Contains("bearer") => Regex.Replace(auth, "bearer |Bearer |bearer_|Bearer_",
                    ""),
                false => auth,
                _ => token
            };

            return token;
        }

        #endregion
    }
}
