using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SurveyApplication.Application.Features.Accounts.Requests.Queries;
using SurveyApplication.Domain.Common;
using SurveyApplication.Domain.Common.Identity;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Domain.Models;
using SurveyApplication.Utility.Constants;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.Application.Features.Accounts.Handlers.Queries;

public class LoginRequestHandler : BaseMasterFeatures, IRequestHandler<LoginRequest, AuthResponse>
{
    private readonly JwtSettings _jwtSettings;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public LoginRequestHandler(ISurveyRepositoryWrapper surveyRepository,
        UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwtSettings,
        SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager) : base(surveyRepository)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public async Task<AuthResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Email);
        if (user == null)
        {
            if (request.Email == AccountAdmin.UserName)
            {
                var hasher = new PasswordHasher<ApplicationUser>();
                var userAdmin = new ApplicationUser
                {
                    Id = AccountAdmin.Uid,
                    Email = AccountAdmin.Email,
                    NormalizedEmail = AccountAdmin.Email.ToUpper(),
                    Name = AccountAdmin.Name,
                    UserName = AccountAdmin.UserName,
                    NormalizedUserName = AccountAdmin.UserName.ToUpper(),
                    PasswordHash = hasher.HashPassword(null!, AccountAdmin.Password),
                    EmailConfirmed = true,
                    Address = AccountAdmin.Address
                };
                await _userManager.CreateAsync(userAdmin);
                var roleAdmin = new IdentityRole
                {
                    Id = RoleAdmin.Id,
                    Name = RoleAdmin.Name,
                    NormalizedName = RoleAdmin.Name.ToUpper()
                };
                await _roleManager.CreateAsync(roleAdmin);
                foreach (var claimModule in MapEnum.MatrixPermission)
                {
                    await _roleManager.AddClaimAsync(roleAdmin, new Claim((claimModule.Key).ToString(), JsonExtensions.SerializeToJson(claimModule.Value), JsonClaimValueTypes.JsonArray));
                }

                await _userManager.AddToRoleAsync(userAdmin, RoleAdmin.Name);
                user = await _userManager.FindByNameAsync(AccountAdmin.UserName);
            }
            else
            {
                throw new Exception($"User with {request.Email} not found.");
            }
        }

        var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false);
        if (!result.Succeeded) throw new Exception($"Credentials for '{request.Email} aren't valid'.");

        var jwtSecurityToken = await GenerateToken(user);
        var response = new AuthResponse
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email,
            UserName = user.UserName
        };

        return response;
    }

    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var userClaims = new List<Claim>();
        var roleClaims = new List<Claim>();
        foreach (var role in roles)
        {
            roleClaims.Add(new Claim(ClaimTypes.Role, role));
            var roleClaim = await _roleManager.GetClaimsAsync(await _roleManager.FindByNameAsync(role));
            userClaims.AddRange(roleClaim);
        }

        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomAuth.Uid, user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }
}