using MediatR;
using SurveyApplication.Domain.Common.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.Accounts.Requests.Commands
{
    public class RegisterCommand : IRequest<BaseCommandResponse>
    {
        public string Address { get; set; }

        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [MinLength(6)]
        public string PasswordConfirmed { get; set; }
        public List<MatrixPermission>? MatrixPermission { get; set; }

        public List<string> LstRoleName { get; set; }
    }
}
