using MediatR;
using Microsoft.AspNetCore.Http;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;
using SurveyApplication.Domain.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands
{
    public class UploadFileCommand : IRequest<BaseCommandResponse>
    {
        public UploadFileDto? UploadFileDto { get; set; }
    }
}
