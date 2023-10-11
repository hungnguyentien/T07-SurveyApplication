using MediatR;
using SurveyApplication.Application.DTOs.Account;
using SurveyApplication.Application.DTOs.DonViAndNguoiDaiDien;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.Accounts.Requests.Queries
{
    public class GetAccountDetailRequest : IRequest<UpdateAccountDto>
    {
        public string Id { get; set; }
    }
}
