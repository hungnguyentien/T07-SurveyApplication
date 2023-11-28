using MediatR;
using SurveyApplication.Application.DTOs.XaPhuong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.XaPhuongs.Requests.Queries
{
    public class GetXaPhuonByQuanHuyenRequest : IRequest<List<XaPhuongDto>>
    {
        public string Id { get; set; }
    }
}
