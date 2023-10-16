using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SurveyApplication.Application.DTOs.Account.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.Accounts.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.Accounts.Handlers.Commands
{
    public class UpdateAccountCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateAccountCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Domain.Role> _roleManager;

        public UpdateAccountCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper, UserManager<ApplicationUser> userManager,
            RoleManager<Domain.Role> roleManager) : base( surveyRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<BaseCommandResponse> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateAccountDtoValidator(_surveyRepo.Account);
            var validatorResult = await validator.ValidateAsync(request.AccountDto);

            

            if (validatorResult.IsValid == false) throw new ValidationException(validatorResult);

            var account = await _surveyRepo.Account.FirstOrDefaultAsync(x => x.Id == request.AccountDto.Id);
            

            if (request.AccountDto.Img != null && request.AccountDto.Img.Length > 0)
            {
                // Xóa ảnh cũ (nếu có)
                if (!string.IsNullOrEmpty(account.Image))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", account.Image);
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                }

                // Tạo tên file và lưu ảnh mới
                var fileName = DateTime.Now.Ticks + ".jpg";
                request.AccountDto.Image = fileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);
                using (var stream = File.Create(path))
                {
                    await request.AccountDto.Img.CopyToAsync(stream);
                }
            }

            account.Image = request.AccountDto.Image;
            account.Name = request.AccountDto.Name;
            account.Email = request.AccountDto.Email;
            account.NormalizedEmail = request.AccountDto.Email.ToUpper();
            account.Address = request.AccountDto.Address;
            
            
            await _surveyRepo.Account.UpdateAsync(account);
            var role = await _roleManager.FindByIdAsync(request.AccountDto.Id);
            var user = await _userManager.FindByIdAsync(request.AccountDto.Id);        
            if (account != null)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);

                foreach (var roleName in currentRoles)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }


                foreach (var roleName in request.AccountDto.LstRoleName)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }

            }

            if (account != null)
            {
                // Lấy danh sách quyền cũ của vai trò
                var existingClaims = await _userManager.GetClaimsAsync(account);

                // Xóa tất cả các quyền cũ
                foreach (var claim in existingClaims)
                {
                    await _userManager.RemoveClaimAsync(account, claim);
                }

                // Thêm các quyền mới
                foreach (var claimModule in request.AccountDto.MatrixPermission)
                {
                    await _userManager.AddClaimAsync(account, new Claim(claimModule.Module.ToString(), JsonExtensions.SerializeToJson(claimModule.LstPermission.Select(x => x.Value)), JsonClaimValueTypes.JsonArray));
                }
            }


            await _surveyRepo.SaveAync();

            return new BaseCommandResponse("Sửa thành công!");
        }
    }
}
