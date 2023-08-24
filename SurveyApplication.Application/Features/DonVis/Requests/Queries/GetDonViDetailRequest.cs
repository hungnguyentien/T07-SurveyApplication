using MediatR;
using SurveyApplication.Application.DTOs.DonVi;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DonVis.Requests.Queries
{
    public class GetDonViDetailRequest : IRequest<DonViDto>
    {
        public int Id { get; set; }
    }
}
