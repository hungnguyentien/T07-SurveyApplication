using MediatR;
using SurveyApplication.Application.DTOs.XaPhuong;
using SurveyApplication.Domain.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.XaPhuongs.Requests.Commands
{
    public class UpdateXaPhuongCommand : IRequest<BaseCommandResponse>
    { 
        public UpdateXaPhuongDto? XaPhuongDto { get; set; }
    }
}
