using MediatR;
using SurveyApplication.Application.DTOs.DonVi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DonVis.Requests.Commands
{
    public class UpdateDonViCommand : IRequest<Unit>
    { 
        public UpdateDonViDto? DonViDto { get; set; }
    }
}
