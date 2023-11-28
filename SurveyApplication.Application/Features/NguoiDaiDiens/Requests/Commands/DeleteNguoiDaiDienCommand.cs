using MediatR;
using SurveyApplication.Domain.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Commands
{
    public class DeleteNguoiDaiDienCommand : IRequest<BaseCommandResponse>
    {
        public List<int> Ids { get; set; }
    }
}
