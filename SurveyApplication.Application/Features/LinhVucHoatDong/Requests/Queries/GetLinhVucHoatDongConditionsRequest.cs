using MediatR;
using SurveyApplication.Application.DTOs.LinhVucHoatDong;
using SurveyApplication.Domain.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Queries
{
    public class GetLinhVucHoatDongConditionsRequest : IRequest<BaseQuerieResponse<LinhVucHoatDongDto>>
    {
        public List<LinhVucHoatDongDto> Data { get; set; }
        public int PageCount { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string Keyword { get; set; }

        public LinhVucHoatDongDto LinhVucHoatDongDto { get; set; }
    }
}
