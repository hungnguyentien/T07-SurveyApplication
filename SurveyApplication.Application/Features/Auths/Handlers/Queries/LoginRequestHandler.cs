using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.Auth;
using SurveyApplication.Application.Features.Auths.Requests.Queries;
using SurveyApplication.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.Auths.Handlers.Queries
{
    public class LoginRequestHandler : IRequestHandler<LoginRequest, AuthDto>
    {
        private readonly IAuthRepository _AuthRepository;
        private readonly IMapper _mapper;

        public LoginRequestHandler(IAuthRepository AuthRepository, IMapper mapper)
        {
            _AuthRepository = AuthRepository;
            _mapper = mapper;
        }

        public async Task<AuthDto> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var AuthRepository = await _AuthRepository.Login(request);
            return _mapper.Map<AuthDto>(AuthRepository);
        }
    }
}
