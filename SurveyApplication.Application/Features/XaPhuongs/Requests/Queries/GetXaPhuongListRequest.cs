using MediatR;
using SurveyApplication.Application.DTOs.XaPhuong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.XaPhuongs.Requests.Queries
{
    public class GetXaPhuongListRequest : IRequest<List<XaPhuongDto>>
    {
    }
   
}
