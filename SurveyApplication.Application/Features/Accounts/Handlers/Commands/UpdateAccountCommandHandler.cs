using MediatR;
using Microsoft.AspNetCore.Identity;
using SurveyApplication.Application.DTOs.Account.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.Accounts.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SurveyApplication.Application.Features.Accounts.Handlers.Commands
{
    public class UpdateAccountCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateAccountCommand, BaseCommandResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateAccountCommandHandler(ISurveyRepositoryWrapper surveyRepository, UserManager<ApplicationUser> userManager) : base(surveyRepository)
        {
            _userManager = userManager;
        }

        public async Task<BaseCommandResponse> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateAccountDtoValidator(_surveyRepo.Account);
            if (request.AccountDto != null)
            {
                var validatorResult = await validator.ValidateAsync(request.AccountDto, cancellationToken);
                if (validatorResult.IsValid == false) throw new ValidationException(validatorResult);
            }

            var account = await _surveyRepo.Account.FirstOrDefaultAsync(x => request.AccountDto != null && x.Id == request.AccountDto.Id);
            if (request.AccountDto is { Img.Length: > 0 })
            {
                // Xóa ảnh cũ (nếu có)
                if (!string.IsNullOrEmpty(account?.Image))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", account.Image);
                    if (File.Exists(oldImagePath))
                        File.Delete(oldImagePath);
                }

                // Tạo tên file và lưu ảnh mới
                var fileName = DateTime.Now.Ticks + ".jpg";
                request.AccountDto.Image = fileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);
                await using var stream = File.Create(path);
                await request.AccountDto.Img.CopyToAsync(stream, cancellationToken);
            }

            if (request.AccountDto != null)
            {
                if (account != null)
                {
                    account.Image = request.AccountDto.Image;
                    account.Name = request.AccountDto.Name;
                    account.Email = request.AccountDto.Email;
                    if (request.AccountDto.Email != null) account.NormalizedEmail = request.AccountDto.Email.ToUpper();
                    account.Address = request.AccountDto.Address;
                    await _surveyRepo.Account.UpdateAsync(account);
                    var user = await _userManager.FindByIdAsync(request.AccountDto.Id);
                    var currentRoles = await _userManager.GetRolesAsync(user);
                    if (currentRoles != null)
                        foreach (var roleName in currentRoles)
                            await _userManager.RemoveFromRoleAsync(user, roleName);

                    if (request.AccountDto.LstRoleName != null)
                        foreach (var roleName in request.AccountDto.LstRoleName)
                            await _userManager.AddToRoleAsync(user, roleName);

                    // Lấy danh sách quyền cũ của vai trò
                    var existingClaims = await _userManager.GetClaimsAsync(account);

                    // Xóa tất cả các quyền cũ
                    foreach (var claim in existingClaims)
                        await _userManager.RemoveClaimAsync(account, claim);

                    // Thêm các quyền mới
                    if (request.AccountDto.MatrixPermission != null)
                        foreach (var claimModule in request.AccountDto.MatrixPermission)
                        {
                            await _userManager.AddClaimAsync(account,
                                new Claim(claimModule.Module.ToString(),
                                    JsonExtensions.SerializeToJson(claimModule.LstPermission.Select(x => x.Value)),
                                    JsonClaimValueTypes.JsonArray));
                        }
                }
            }

            await _surveyRepo.SaveAync();
            return new BaseCommandResponse("Sửa thành công!");
        }
    }
}
