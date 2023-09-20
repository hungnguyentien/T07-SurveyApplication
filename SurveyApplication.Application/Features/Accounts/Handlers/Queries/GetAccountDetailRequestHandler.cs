using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.Account;
using SurveyApplication.Application.Features.Accounts.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.Accounts.Handlers.Queries
{
    public class GetAccountDetailRequestHandler : BaseMasterFeatures, IRequestHandler<GetAccountDetailRequest, AccountDto>
    {
        private readonly IMapper _mapper;

        public GetAccountDetailRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
            surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<AccountDto> Handle(GetAccountDetailRequest request,
            CancellationToken cancellationToken)
        {
            var accountRepository = await _surveyRepo.Account.GetAsync(request.Id);
            return _mapper.Map<AccountDto>(accountRepository);
        }
    }
}
