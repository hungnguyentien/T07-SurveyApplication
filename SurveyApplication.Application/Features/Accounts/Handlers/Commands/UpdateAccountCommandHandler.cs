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

            var account = await _surveyRepo.Account.GetAsync(request.AccountDto?.Id ?? "");
            request.AccountDto.NormalizedEmail = request.AccountDto.Email.ToUpper();
            request.AccountDto.NormalizedUserName = request.AccountDto.UserName.ToUpper();
            request.AccountDto.EmailConfirmed = true;
            request.AccountDto.PasswordHash = account.PasswordHash;
            request.AccountDto.LockoutEnabled = true;

            _mapper.Map(request.AccountDto, account);
            await _surveyRepo.Account.Update(account);
            await _surveyRepo.SaveAync();
            return new BaseCommandResponse("Sửa thành công!");
        }
    }
}
