using MediatR;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DotKhaoSats.Requests.Queries
{
    public class GetGuiEmailListRequest : IRequest<List<DotKhaoSatDto>>
    {
    }
}
