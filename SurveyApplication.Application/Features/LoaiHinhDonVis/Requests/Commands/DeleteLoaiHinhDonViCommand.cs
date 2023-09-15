using MediatR;
using SurveyApplication.Domain.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands
{
    public class DeleteLoaiHinhDonViCommand : IRequest<BaseCommandResponse>
    {
        public List<int> Ids { get; set; }
    }
}
