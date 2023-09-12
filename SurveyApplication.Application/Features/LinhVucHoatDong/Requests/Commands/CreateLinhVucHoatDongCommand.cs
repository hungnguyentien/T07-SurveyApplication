using MediatR;
using SurveyApplication.Application.DTOs.LinhVucHoatDong;
using SurveyApplication.Domain.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Commands
{
    public class CreateLinhVucHoatDongCommand : IRequest<BaseCommandResponse>
    {
        public CreateLinhVucHoatDongDto? LinhVucHoatDongDto { get; set; }
    }
}
