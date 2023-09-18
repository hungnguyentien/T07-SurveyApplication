using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.Account;
using SurveyApplication.Application.Features.Accounts.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.Application.Features.Accounts.Handlers.Queries
{
    public class GetUserConditionsRequestHandler : BaseMasterFeatures, IRequestHandler<GetUserConditionsRequest, BaseQuerieResponse<AccountDto>>
    {
        private readonly IMapper _mapper;
        public GetUserConditionsRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseQuerieResponse<AccountDto>> Handle(GetUserConditionsRequest request, CancellationToken cancellationToken)
        {
            var lstAccount = await _surveyRepo.Account.GetByConditionsQueriesResponse(request.PageIndex, request.PageSize,
                x => (string.IsNullOrEmpty(request.Keyword) || x.UserName.Contains(request.Keyword)) &&
                     x.ActiveFlag == (int)EnumCommon.ActiveFlag.Active && !x.Deleted, request.OrderBy ?? "");
            var result = _mapper.Map<List<AccountDto>>(lstAccount);
            return new BaseQuerieResponse<AccountDto>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Keyword = request.Keyword,
                TotalFilter = lstAccount.TotalFilter,
                Data = result
            };
        }
    }
}
