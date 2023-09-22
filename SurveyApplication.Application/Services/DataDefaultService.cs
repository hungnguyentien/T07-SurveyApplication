using SurveyApplication.Application.Features;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Domain.Models;
using SurveyApplication.Utility.Constants;
using SurveyApplication.Utility.Enums;
using SurveyApplication.Application.Services.Interfaces;
using SurveyApplication.Utility.LogUtils;
using System;

namespace SurveyApplication.Application.Services
{
    public class DataDefaultService : BaseMasterFeatures, IDataDefaultService
    {
        private readonly ILoggerManager _logger;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public DataDefaultService(ISurveyRepositoryWrapper surveyRepository,
            UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager, ILoggerManager logger) : base(surveyRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task DataAdmin()
        {
            try
            {
                var user = await _userManager.FindByNameAsync(AccountAdmin.UserName);
                if (user == null)
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
                        Address = AccountAdmin.Address,
                        ActiveFlag = (int)EnumCommon.ActiveFlag.Active,
                        Deleted = false
                    };
                    await _userManager.CreateAsync(userAdmin);
                    var roleAdmin = new Role
                    {
                        Id = RoleAdmin.Id,
                        Name = RoleAdmin.Name,
                        NormalizedName = RoleAdmin.Name.ToUpper(),
                        ActiveFlag = (int)EnumCommon.ActiveFlag.Active,
                        Deleted = false
                    };
                    await _roleManager.CreateAsync(roleAdmin);
                    foreach (var claimModule in MapEnum.MatrixPermission)
                    {
                        await _roleManager.AddClaimAsync(roleAdmin, new Claim((claimModule.Key).ToString(), JsonExtensions.SerializeToJson(claimModule.Value), JsonClaimValueTypes.JsonArray));
                    }

                    await _userManager.AddToRoleAsync(userAdmin, RoleAdmin.Name);
                }
                else
                {
                    var roleAdmin = await _roleManager.FindByIdAsync(RoleAdmin.Id);
                    if (roleAdmin != null)
                    {
                        var lstPermission = await _roleManager.GetClaimsAsync(roleAdmin) ?? new List<Claim>();
                        if (MapEnum.MatrixPermission.Any(x =>
                                !lstPermission.Select(p => p.Type).Contains(x.Key.ToString())))
                        {
                            foreach (var claimModule in MapEnum.MatrixPermission)
                            {
                                await _roleManager.RemoveClaimAsync(roleAdmin, new Claim((claimModule.Key).ToString(), JsonExtensions.SerializeToJson(claimModule.Value), JsonClaimValueTypes.JsonArray));
                                await _roleManager.AddClaimAsync(roleAdmin, new Claim((claimModule.Key).ToString(), JsonExtensions.SerializeToJson(claimModule.Value), JsonClaimValueTypes.JsonArray));

                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
        }
    }
}
