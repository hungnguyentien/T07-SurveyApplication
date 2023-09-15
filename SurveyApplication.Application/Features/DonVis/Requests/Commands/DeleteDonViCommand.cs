using MediatR;
using SurveyApplication.Domain.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DonVis.Requests.Commands
{
    public class DeleteDonViCommand : IRequest<BaseCommandResponse>
    {
        public List<int> Ids { get; set; }
    }
}
