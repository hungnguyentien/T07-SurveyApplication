using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SurveyApplication.Application.Features;
using SurveyApplication.Application.Services.Interfaces;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Domain.Models;
using SurveyApplication.Utility.Constants;
using SurveyApplication.Utility.Enums;
using SurveyApplication.Utility.LogUtils;

namespace SurveyApplication.Application.Services;

public class DataDefaultService : BaseMasterFeatures, IDataDefaultService
{
    private readonly ILoggerManager _logger;
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public DataDefaultService(ISurveyRepositoryWrapper surveyRepository,
        UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager,
        ILoggerManager logger) : base(surveyRepository)
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
                    await _roleManager.AddClaimAsync(roleAdmin,
                        new Claim(claimModule.Key.ToString(), JsonExtensions.SerializeToJson(claimModule.Value),
                            JsonClaimValueTypes.JsonArray));

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
                        foreach (var claimModule in MapEnum.MatrixPermission)
                        {
                            await _roleManager.RemoveClaimAsync(roleAdmin,
                                new Claim(claimModule.Key.ToString(), JsonExtensions.SerializeToJson(claimModule.Value),
                                    JsonClaimValueTypes.JsonArray));
                            await _roleManager.AddClaimAsync(roleAdmin,
                                new Claim(claimModule.Key.ToString(), JsonExtensions.SerializeToJson(claimModule.Value),
                                    JsonClaimValueTypes.JsonArray));
                        }
                }
            }

            if (!await _surveyRepo.Module.AnyAsync(x => !x.Deleted)) await SeedModule();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex);
        }
    }

    private async Task SeedModule()
    {
        var lstModule = new List<Module>
        {
            new()
            {
                Id = 0,
                Name = "Dashboard",
                RouterLink = "admin/dashboard",
                Icon = "fas fa-fw fa-tachometer-alt",
                CodeModule = EnumModule.Code.Dashboard.ToString(),
                IdParent = 0,
                Priority = 0
            },
            new()
            {
                Id = 0,
                Name = "Thống kê báo cáo",
                RouterLink = "admin/thong-ke-khao-sat",
                Icon = "fas fa-fw fa-chart-area",
                CodeModule = EnumModule.Code.TkKs.ToString(),
                IdParent = 0,
                Priority = 1
            },
            new()
            {
                Id = 0,
                Name = "Quản lý câu hỏi",
                RouterLink = "admin/quan-ly-cau-hoi",
                Icon = "fa fa-question-circle",
                CodeModule = EnumModule.Code.QlCh.ToString(),
                IdParent = 0,
                Priority = 2
            },
            new()
            {
                Id = 0,
                Name = "Quản lý khảo sát",
                RouterLink = "#",
                Icon = "fas fa-fw fa-folder",
                CodeModule = "",
                IdParent = 0,
                Priority = 3
            },
            new()
            {
                Id = 0,
                Name = "Quản lý đợt khảo sát",
                RouterLink = "admin/dot-khao-sat",
                Icon = "",
                CodeModule = EnumModule.Code.QlDks.ToString(),
                IdParent = 4,
                Priority = 4
            },
            new()
            {
                Id = 0,
                Name = "Quản lý bảng khảo sát",
                RouterLink = "admin/bang-khao-sat",
                Icon = "",
                CodeModule = EnumModule.Code.QlKs.ToString(),
                IdParent = 4,
                Priority = 5
            },
            new()
            {
                Id = 0,
                Name = "Quản lý gửi email",
                RouterLink = "admin/gui-email",
                Icon = "",
                CodeModule = EnumModule.Code.QlGm.ToString(),
                IdParent = 4,
                Priority = 6
            },
            new()
            {
                Id = 0,
                Name = "Quản lý đối tượng khảo sát",
                RouterLink = "#",
                Icon = "fa fa-user",
                CodeModule = "",
                IdParent = 0,
                Priority = 7
            },
            new()
            {
                Id = 0,
                Name = "Quản lý đơn vị",
                RouterLink = "admin/quan-ly-don-vi",
                Icon = "",
                CodeModule = EnumModule.Code.QlDv.ToString(),
                IdParent = 8,
                Priority = 8
            },
            new()
            {
                Id = 0,
                Name = "Quản lý loại hình đơn vị",
                RouterLink = "admin/loai-hinh-don-vi",
                Icon = "",
                CodeModule = EnumModule.Code.QlLhDv.ToString(),
                IdParent = 8,
                Priority = 9
            },
            new()
            {
                Id = 0,
                Name = "Quản lý danh mục",
                RouterLink = "#",
                Icon = "pi pi-bars",
                CodeModule = "",
                IdParent = 0,
                Priority = 10
            },
            new()
            {
                Id = 0,
                Name = "Lĩnh vực hoạt động",
                RouterLink = "admin/linh-vuc-hoat-dong",
                Icon = "",
                CodeModule = EnumModule.Code.LvHd.ToString(),
                IdParent = 11,
                Priority = 11
            },
            new()
            {
                Id = 0,
                Name = "Quản lý Tỉnh Thành",
                RouterLink = "admin/tinh-thanh",
                Icon = "",
                CodeModule = EnumModule.Code.QlTt.ToString(),
                IdParent = 11,
                Priority = 12
            },
            new()
            {
                Id = 0,
                Name = "Quản lý Huyện/Quận",
                RouterLink = "admin/quan-huyen",
                Icon = "",
                CodeModule = EnumModule.Code.QlQh.ToString(),
                IdParent = 11,
                Priority = 13
            },
            new()
            {
                Id = 0,
                Name = "Quản lý Xã/Phường",
                RouterLink = "admin/xa-phuong",
                Icon = "",
                CodeModule = EnumModule.Code.QlPx.ToString(),
                IdParent = 11,
                Priority = 14
            },
            new()
            {
                Id = 0,
                Name = "Quản lý nhóm quyền",
                RouterLink = "admin/nhom-quyen",
                Icon = "",
                CodeModule = EnumModule.Code.QlNq.ToString(),
                IdParent = 19,
                Priority = 16
            },
            new()
            {
                Id = 0,
                Name = "Quản lý tài khoản",
                RouterLink = "admin/tai-khoan",
                Icon = "",
                CodeModule = EnumModule.Code.QlTk.ToString(),
                IdParent = 19,
                Priority = 15
            },
            new()
            {
                Id = 0,
                Name = "Quản lý Module",
                RouterLink = "admin/quan-ly-module",
                Icon = "",
                CodeModule = EnumModule.Code.QlMd.ToString(),
                IdParent = 19,
                Priority = 17
            },
            new()
            {
                Id = 0,
                Name = "Quản trị hệ thống",
                RouterLink = "#",
                Icon = "pi pi-spin pi-cog",
                CodeModule = "",
                IdParent = 0,
                Priority = 18
            },
            new()
            {
                Id = 0,
                Name = "Quản lý IpAddress",
                RouterLink = "admin/quan-ly-ipaddress",
                Icon = "",
                CodeModule = EnumModule.Code.QlIp.ToString(),
                IdParent = 19,
                Priority = 19
            },
            new()
            {
                Id = 0,
                Name = "Quản lý sao lưu phục hồi",
                RouterLink = "admin/quan-ly-sao-luu-khoi-phuc",
                Icon = "",
                CodeModule = EnumModule.Code.Qlsl.ToString(),
                IdParent = 19,
                Priority = 20
            }
        };
        await _surveyRepo.Module.InsertAsync(lstModule);
        await _surveyRepo.SaveAync();
    }
}