using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SurveyApplication.Application.DTOs.Account;
using SurveyApplication.Application.DTOs.Role;
using SurveyApplication.Application.Features.Accounts.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Domain.Models;
using SurveyApplication.Utility.Enums;
using SurveyApplication.Utility;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.Accounts.Handlers.Queries
{
    public class GetAccountDetailRequestHandler : BaseMasterFeatures, IRequestHandler<GetAccountDetailRequest, UpdateAccountDto>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public GetAccountDetailRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper,UserManager<ApplicationUser> userManager) : base(
            surveyRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        //public async Task<AccountDto> Handle(GetAccountDetailRequest request,
        //    CancellationToken cancellationToken)
        //{
        //    var accountRepository = await _surveyRepo.Account.GetAsync(request.Id);
        //    return _mapper.Map<AccountDto>(accountRepository);
        //}
        public async Task<UpdateAccountDto> Handle(GetAccountDetailRequest request, CancellationToken cancellationToken)
        {
            var userId = request.Id;
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            var roleList = roles.ToList();

            var accountDto = _mapper.Map<UpdateAccountDto>(user);
            accountDto.LstRoleName = roleList;



            var claims = await _surveyRepo.Account.FirstOrDefaultAsync(x => !x.Deleted && x.Id == userId) ?? new ApplicationUser();
            var rs = new UpdateAccountDto
            {
                Id = claims.Id,
                Name = claims.Name,
                Image = claims.Image,
                Address = claims.Address,
                Img = claims.Img,
            };
            if (string.IsNullOrEmpty(claims.Id)) return rs;
            {
                var lstPermission = await _userManager.GetClaimsAsync(claims) ?? new List<Claim>();
                rs.MatrixPermission = lstPermission.Select(x => new MatrixPermission
                {
                    Module = Convert.ToInt32(x.Type),
                    NameModule = EnumUltils.GetDescriptionValue<EnumModule.Code>().GetValueOrDefault(Convert.ToInt32(x.Type), ""),
                    LstPermission = JsonExtensions.DeserializeFromJson<List<int>>(x.Value).Select(v => new LstPermission
                    {
                        Value = v,
                        Name = EnumUltils.GetDescriptionValue<EnumPermission.Type>().GetValueOrDefault(v, "")
                    }).ToList()
                }).ToList();
                accountDto.MatrixPermission = rs.MatrixPermission;

            }




            return accountDto;

        }
    }
}
