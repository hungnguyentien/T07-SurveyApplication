using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SurveyApplication.Application.DTOs.Account;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.Enums;
using SurveyApplication.Application.Features.Accounts.Requests.Queries;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries;
using SurveyApplication.Domain.Common;
using SurveyApplication.Domain.Common.Identity;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.Accounts.Handlers.Queries
{
    public class LoginRequestHandler : BaseMasterFeatures, IRequestHandler<LoginRequest, AuthResponse>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtSettings _jwtSettings;
        public LoginRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper, UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwtSettings, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager) : base(surveyRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<AuthResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            //var hasher = new PasswordHasher<ApplicationUser>();
            //var userAdmin = new ApplicationUser
            //{
            //    Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
            //    Email = "admin@gmail.com",
            //    NormalizedEmail = "ADMIN@GAMAIL.COM",
            //    FirstName = "System",
            //    LastName = "Admin",
            //    UserName = "admin",
            //    NormalizedUserName = "ADMIN",
            //    PasswordHash = hasher.HashPassword(null, "123qwe"),
            //    EmailConfirmed = true
            //};
            //var t = await _userManager.CreateAsync(userAdmin);
            //var role = await _roleManager.CreateAsync(new IdentityRole
            //{
            //    Id = "cbc43a8e-f7bb-4445-baaf-1add431ffbbf",
            //    Name = "Administrator",
            //    NormalizedName = "ADMINISTRATOR"
            //});
            //await _userManager.AddToRoleAsync(userAdmin, "Administrator");

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                if (request.Email == "admin@gmail.com")
                {
                    var hasher = new PasswordHasher<ApplicationUser>();
                    var userAdmin = new ApplicationUser
                    {
                        Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                        Email = "admin@gmail.com",
                        NormalizedEmail = "ADMIN@GAMAIL.COM",
                        FirstName = "System",
                        LastName = "Admin",
                        UserName = "admin",
                        NormalizedUserName = "ADMIN",
                        PasswordHash = hasher.HashPassword(null, "123qwe"),
                        EmailConfirmed = true
                    };
                    await _userManager.CreateAsync(userAdmin);
                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Id = "cbc43a8e-f7bb-4445-baaf-1add431ffbbf",
                        Name = "Administrator",
                        NormalizedName = "ADMINISTRATOR"
                    });
                    await _userManager.AddToRoleAsync(userAdmin, "Administrator");
                    user = await _userManager.FindByEmailAsync(request.Email);
                }
                else
                {
                    throw new Exception($"User with {request.Email} not found.");
                }
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new Exception($"Credentials for '{request.Email} aren't valid'.");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            AuthResponse response = new AuthResponse
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

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
                var roleClaim = await _roleManager.GetClaimsAsync(await _roleManager.FindByNameAsync(roles[i]));
                userClaims.AddRange(roleClaim);
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                //new Claim(CustomClaimTypes.Uid, user.Id)
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}
