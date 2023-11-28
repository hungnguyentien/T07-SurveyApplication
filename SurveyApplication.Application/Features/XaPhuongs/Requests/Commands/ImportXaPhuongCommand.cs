using MediatR;
using Microsoft.AspNetCore.Http;
using SurveyApplication.Domain.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.XaPhuongs.Requests.Commands
{
    public class ImportXaPhuongCommand : IRequest<BaseCommandResponse>
    {
        public IFormFile? File { get; set; }
    }
}
