using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.Account.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.Accounts.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.Accounts.Handlers.Commands
{
    public class UpdateAccountCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateAccountCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;

        public UpdateAccountCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
            surveyRepository)
        {
            _mapper = mapper;
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
                var fileName = DateTime.Now.Ticks.ToString() + ".jpg";
                account.Image = fileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);
                using (var stream = File.Create(path))
                {
                    await request.AccountDto.Img.CopyToAsync(stream);
                }
            }

            request.AccountDto.NormalizedEmail = request.AccountDto.Email.ToUpper();
            request.AccountDto.NormalizedUserName = request.AccountDto.UserName.ToUpper();
            request.AccountDto.EmailConfirmed = true;
            request.AccountDto.PasswordHash = account.PasswordHash;
            request.AccountDto.LockoutEnabled = true;
            request.AccountDto.Image = account.Image;

            _mapper.Map(request.AccountDto, account);
            await _surveyRepo.Account.Update(account);
            await _surveyRepo.SaveAync();
            return new BaseCommandResponse("Sửa thành công!");
        }
    }
}
