using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.QuanHuyen;
using SurveyApplication.Application.Features.QuanHuyens.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.QuanHuyens.Handlers.Queries
{
   
    public class GetQuanHuyenConditionsRequestHandler : BaseMasterFeatures, IRequestHandler<GetQuanHuyenConditionsRequest, BaseQuerieResponse<QuanHuyenDto>>
    {
        private readonly IMapper _mapper;
        public GetQuanHuyenConditionsRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseQuerieResponse<QuanHuyenDto>> Handle(GetQuanHuyenConditionsRequest request, CancellationToken cancellationToken)
        {
            var query = from d in _surveyRepo.QuanHuyen.GetAllQueryable()
                        join b in _surveyRepo.TinhTp.GetAllQueryable()
                        on d.ParentCode equals b.Code
                        where d.Code.Contains(request.Keyword) || d.Name.Contains(request.Keyword) ||
                             b.Code.Contains(request.Keyword) || b.Name.Contains(request.Keyword)
                        select new QuanHuyenDto
                        {
                            Id = d.Id,
                            Code = d.Code,
                            Name = d.Name,

                            parent_code = d.ParentCode,
                            NameTinhTp = b.Name,
                        };
            var totalCount = await query.LongCountAsync();
            var pageCount = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            var pageResults = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

            return new BaseQuerieResponse<QuanHuyenDto>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Keyword = request.Keyword,
                TotalFilter = totalCount,
                Data = pageResults
            };
        }
    }

}
