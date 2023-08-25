using MediatR;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.GuiEmails.Requests.Commands
{
    
    
    public class CreatGuiEmailCommand : IRequest<BaseCommandResponse>
    {
        public CreateGuiEmailDto? GuiEmailDto { get; set; }
    }

}
