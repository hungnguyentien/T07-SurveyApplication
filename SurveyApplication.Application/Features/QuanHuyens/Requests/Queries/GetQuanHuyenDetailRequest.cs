using MediatR;
using SurveyApplication.Application.DTOs.QuanHuyen;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.QuanHuyens.Requests.Queries
{
    public class GetQuanHuyenDetailRequest : IRequest<QuanHuyenDto>
    {
        public int Id { get; set; }
    }
}
