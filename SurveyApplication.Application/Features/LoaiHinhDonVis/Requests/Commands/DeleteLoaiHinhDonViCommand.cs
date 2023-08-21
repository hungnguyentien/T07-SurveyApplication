using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands
{
    public class DeleteLoaiHinhDonViCommand : IRequest
    {
        public string? Maloaihinh { get; set; }
    }
}
