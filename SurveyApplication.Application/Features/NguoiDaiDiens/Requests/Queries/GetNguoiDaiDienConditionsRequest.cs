using MediatR;
using SurveyApplication.Application.DTOs.NguoiDaiDien;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Queries
{
    
    public class GetNguoiDaiDienConditionsRequest : IRequest<List<NguoiDaiDienDto>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Keyword { get; set; }
    }
}
