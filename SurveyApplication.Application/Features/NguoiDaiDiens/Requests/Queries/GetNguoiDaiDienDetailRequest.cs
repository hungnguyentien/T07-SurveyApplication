using MediatR;
using SurveyApplication.Application.DTOs.NguoiDaiDien;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Queries
{
    public class GetNguoiDaiDienDetailRequest : IRequest<NguoiDaiDienDto>
    {
        public int Id { get; set; }
    }
}
