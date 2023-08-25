using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries
{
    public class GetLoaiHinhDonViDetailRequest : IRequest<LoaiHinhDonViDto>
    {
        public int Id { get; set; }
    }
}
