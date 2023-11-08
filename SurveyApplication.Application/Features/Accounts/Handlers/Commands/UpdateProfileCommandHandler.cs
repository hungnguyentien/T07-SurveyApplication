using MediatR;
using Microsoft.AspNetCore.Identity;
using SurveyApplication.Application.DTOs.Account.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.Accounts.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Domain.Models;

namespace SurveyApplication.Application.Features.Accounts.Handlers.Commands
{
 
    public class UpdateProfileCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateProfileCommand, BaseCommandResponse>
    {
        public UpdateProfileCommandHandler(ISurveyRepositoryWrapper surveyRepository, UserManager<ApplicationUser> userManager) : base(surveyRepository)
        {
        }

        public async Task<BaseCommandResponse> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
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

            if (request.AccountDto == null) return new BaseCommandResponse("Sửa thành công!");
            if (account == null) return new BaseCommandResponse("Sửa thành công!");
            account.Image = request.AccountDto.Image;
            account.Name = request.AccountDto.Name;
            account.Email = request.AccountDto.Email;
            if (request.AccountDto.Email != null) account.NormalizedEmail = request.AccountDto.Email.ToUpper();
            account.Address = request.AccountDto.Address;
            await _surveyRepo.Account.UpdateAsync(account);
            await _surveyRepo.SaveAync();
            return new BaseCommandResponse("Sửa thành công!");
        }
    }
}


