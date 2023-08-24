using MediatR;
using SurveyApplication.Application.DTOs.DonVi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DonVis.Requests.Queries
{
    
    public class GetDonViConditionsRequest : IRequest<List<DonViDto>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Keyword { get; set; }
    }
}
