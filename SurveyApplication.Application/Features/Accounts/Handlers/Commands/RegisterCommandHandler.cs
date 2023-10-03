﻿using MediatR;
using SurveyApplication.Application.Features.Accounts.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using Microsoft.AspNetCore.Identity;
using SurveyApplication.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SurveyApplication.Utility.Constants;

namespace SurveyApplication.Application.Features.Accounts.Handlers.Commands
{
    public class RegisterCommandHandler : BaseMasterFeatures, IRequestHandler<RegisterCommand, BaseCommandResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterCommandHandler(ISurveyRepositoryWrapper surveyRepository, UserManager<ApplicationUser> userManager) : base(surveyRepository)
        {
            _userManager = userManager;
        }

        public async Task<BaseCommandResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByNameAsync(request.Register.UserName);
            if (existingUser != null)
                throw new Exception($"Tài khoản '{request.Register.UserName}' đã tồn tại");

            if (request.Register.Img != null && request.Register.Img.Length > 0)
            {
                var fileName = DateTime.Now.Ticks.ToString() + ".jpg";
                request.Register.Image = fileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);
                await using var fileStream = new FileStream(filePath, FileMode.Create);
                await request.Register.Img.CopyToAsync(fileStream, cancellationToken);
            }

            var user = new ApplicationUser
            {
                Email = request.Register.Email,
                NormalizedEmail = request.Register.Email.ToUpper(),
                Name = request.Register.Name,
                UserName = request.Register.UserName,
                NormalizedUserName = request.Register.UserName.ToUpper(),
                EmailConfirmed = true,
                Address = request.Register.Address,
                Image = request.Register.Image,
            };
            var existingEmail = await _userManager.FindByEmailAsync(request.Register.Email);
            if (existingEmail != null) throw new Exception($"Email {request.Register.Email} đã tồn tại!");
            var result = await _userManager.CreateAsync(user, request.Register.Password);
            if (result.Succeeded)
            {
                foreach (var roleName in request.Register.LstRoleName ?? new List<string>())
                    await _userManager.AddToRoleAsync(user, roleName);

                if (request.Register.MatrixPermissionRole == null) return new BaseCommandResponse("Tạo quyền thất bại!");

                foreach (var claimModule in request.Register.MatrixPermissionRole)
                {
                    await _userManager.AddClaimAsync(user,
                        new Claim(claimModule.Module.ToString(),
                            JsonExtensions.SerializeToJson(claimModule.LstPermission.Select(x => x.Value)),
                            JsonClaimValueTypes.JsonArray));
                }

                //foreach (var claimUser in request.Register.MatrixPermissionUser)
                //{
                //    await _userManager.AddClaimAsync(claimUser, new Claim(claimUser.Module.ToString(), JsonExtensions.SerializeToJson(claimUser.LstPermission.Select(x => x.Value)), JsonClaimValueTypes.JsonArray));
                //}

                return new BaseCommandResponse("Tạo tài khoản thành công!");
            }

            throw new Exception($"{result.Errors}");
        }
    }
}
