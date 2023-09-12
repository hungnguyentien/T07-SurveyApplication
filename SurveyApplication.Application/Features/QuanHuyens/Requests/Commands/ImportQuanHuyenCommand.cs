using MediatR;
using Microsoft.AspNetCore.Http;
using SurveyApplication.Application.DTOs.QuanHuyen;
using SurveyApplication.Domain.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.QuanHuyens.Requests.Commands
{
    public class ImportQuanHuyenCommand : IRequest<BaseCommandResponse>
    {
        public IFormFile? File { get; set; }
    }
}
