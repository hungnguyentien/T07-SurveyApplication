using MediatR;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DonVis.Requests.Commands
{
    public class CreateDonViCommand : IRequest<BaseCommandResponse>
    {
        public CreateDonViDto? DonViDto { get; set; }
    }
}
