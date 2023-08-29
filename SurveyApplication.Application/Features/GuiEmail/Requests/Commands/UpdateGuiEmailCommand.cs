using MediatR;

using SurveyApplication.Application.DTOs.GuiEmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.GuiEmails.Requests.Commands
{
   
    public class UpdateGuiEmailCommand : IRequest<Unit>
    {
        public UpdateGuiEmailDto? GuiEmailDto { get; set; }
    }
}
