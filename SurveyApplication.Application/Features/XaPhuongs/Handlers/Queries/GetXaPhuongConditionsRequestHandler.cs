using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.DTOs.XaPhuong;
using SurveyApplication.Application.Features.XaPhuongs.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.XaPhuongs.Handlers.Queries;

public class GetXaPhuongConditionsRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetXaPhuongConditionsRequest, BaseQuerieResponse<XaPhuongDto>>
{
    private readonly IMapper _mapper;

    public GetXaPhuongConditionsRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseQuerieResponse<XaPhuongDto>> Handle(GetXaPhuongConditionsRequest request,
        CancellationToken cancellationToken)
    {
        var query = from d in _surveyRepo.XaPhuong.GetAllQueryable()
            join b in _surveyRepo.QuanHuyen.GetAllQueryable()
                on d.ParentCode equals b.Code
            join o in _surveyRepo.TinhTp.GetAllQueryable()
                on b.ParentCode equals o.Code
            where (d.Code.Contains(request.Keyword) || d.Name.Contains(request.Keyword) ||
                   b.Code.Contains(request.Keyword) || b.Name.Contains(request.Keyword) ||
                   o.Code.Contains(request.Keyword) || o.Name.Contains(request.Keyword)) &&
                  d.Deleted == false
            select new XaPhuongDto
            {
                Id = d.Id,
                Code = d.Code,
                Name = d.Name,
                Type = d.Type,

                parent_code = d.ParentCode,
                NameTinhTp = o.Name,

                CodeQuanHuyen = b.Code,
                NameQuanHuyen = b.Name
            };
        var totalCount = await query.LongCountAsync(cancellationToken);
        var pageResults = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
            .ToListAsync(cancellationToken);
        return new BaseQuerieResponse<XaPhuongDto>
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Keyword = request.Keyword,
            TotalFilter = totalCount,
            Data = pageResults
        };
    }
}