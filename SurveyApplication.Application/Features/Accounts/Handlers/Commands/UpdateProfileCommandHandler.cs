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
 
    public class UpdateProfileCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateAccountCommand, BaseCommandResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateProfileCommandHandler(ISurveyRepositoryWrapper surveyRepository, UserManager<ApplicationUser> userManager) : base(surveyRepository)
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


            await _surveyRepo.SaveAync();
            return new BaseCommandResponse("Sửa thành công!");
        }
    }
}


