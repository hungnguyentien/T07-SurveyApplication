using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.DTOs.StgFile;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Queries
{
    public class DownloadFileRequest : IRequest<StgFileDto>
    {
        public long Id { get; set; }
    }
}
