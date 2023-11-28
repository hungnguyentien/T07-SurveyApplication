using MediatR;
using Microsoft.AspNetCore.Http;
using SurveyApplication.Application.DTOs.TinhTp;
using SurveyApplication.Domain.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.TinhTps.Requests.Commands
{
    public class ImportTinhTpCommand : IRequest<BaseCommandResponse>
    {
        public IFormFile? File { get; set; }
    }
}
