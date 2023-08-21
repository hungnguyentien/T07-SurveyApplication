using MediatR;
using SurveyApplication.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries
{
    public class GetLoaiHinhDonViDetailRequest : IRequest<LoaiHinhDonViDto>
    {
        public string? Maloaihinh { get; set; }
    }
}
