using MediatR;
using SurveyApplication.Application.DTOs.TinhTp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.TinhTps.Requests.Queries
{
    public class GetTinhTpListRequest : IRequest<List<TinhTpDto>>
    {
    }
   
}
