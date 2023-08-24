using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DonVis.Requests.Commands
{
    public class DeleteDonViCommand : IRequest
    {
        public int Id { get; set; }
    }
}
