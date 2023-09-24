using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SurveyApplication.Application.DTOs.NguoiDaiDien;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Queries;
using SurveyApplication.Domain.Common;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using System.Net.WebSockets;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Handlers.Queries
{
    public class GetIpAddressRequestHandler : BaseMasterFeatures, IRequestHandler<GetIpAddressRequest, BaseQuerieResponse<IpAddressDto>>
    {
        public GetIpAddressRequestHandler(ISurveyRepositoryWrapper surveyRepository) : base(surveyRepository)
        {
        }

        public async Task<BaseQuerieResponse<IpAddressDto>> Handle(GetIpAddressRequest request, CancellationToken cancellationToken)
        {
            var query = _surveyRepo.KetQua.GetAllQueryable().Where(x => !string.IsNullOrEmpty(x.IpAddressClient) && JsonSqlExtensions.IsJson(x.IpAddressClient) > 0 && (string.IsNullOrEmpty(request.Keyword) || JsonSqlExtensions.JsonValue(x.IpAddressClient, "$.region").Contains(request.Keyword) || JsonSqlExtensions.JsonValue(x.IpAddressClient, "$.country").Contains(request.Keyword))).Select(x => JsonConvert.DeserializeObject<IpAddressDto>(x.IpAddressClient));
            var result = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync(cancellationToken: cancellationToken);
            return new BaseQuerieResponse<IpAddressDto>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Keyword = request.Keyword,
                TotalFilter = result.LongCount(),
                Data = result
            };
        }
    }
}
