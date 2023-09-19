using MediatR;
using SurveyApplication.Application.DTOs.Role;
using SurveyApplication.Domain.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.Role.Requests.Commands
{
    public class UpdateRoleCommand : IRequest<BaseCommandResponse>
    {
        public UpdateRoleDto? UpdateRoleDto { get; set; }
    }
}
