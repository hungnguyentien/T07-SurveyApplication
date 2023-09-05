using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.Features.Auths.Requests.Queries;
using SurveyApplication.Application.Models.Identity;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Persistence.Repositories
{
    public class AuthRepository : GenericRepository<NguoiDung>, IAuthRepository
    {
        private readonly SurveyApplicationDbContext _dbContext;
        private readonly JwtSettings _jwtSettings;

        public AuthRepository(SurveyApplicationDbContext dbContext, IOptions<JwtSettings> jwtSettings) : base(dbContext)
        {
            _dbContext = dbContext;
            _jwtSettings = jwtSettings.Value;
        }

        public Task<bool> ExistsByMaAuth(string maBangKhaoSat)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResponse> Login(LoginRequest request)
        {
            var user = (from p in _dbContext.NguoiDung

                        join e in _dbContext.NguoiDungVaiTro
                        on p.Id equals e.MaNguoiDung

                        join g in _dbContext.VaiTro
                        on e.MaVaiTro equals g.Id

                        join h in _dbContext.VaiTroQuyen
                        on g.Id equals h.MaVaiTro

                        join k in _dbContext.Quyen
                        on h.MaQuyen equals k.Id

                        where p.UserName == request.UserName && p.PassWord == request.PassWord && p.ActiveFlag == 1

                        select new
                        {
                            UserName = p.UserName,
                            PassWord = p.PassWord,
                            Email = p.Email,
                            MaNguoiDung = p.MaNguoiDung,
                            TenVaiTro = g.TenVaiTro,
                            TenQuyen = k.TenQuyen
                        }).ToList();

            if (user != null && user.Count > 0)
            {
                var firstUser = user[0];

                List<Claim> userClaims = new List<Claim>()
                {
                new Claim(ClaimTypes.NameIdentifier, firstUser.MaNguoiDung.ToString()),
                new Claim(ClaimTypes.Role, firstUser.TenVaiTro)
                };

                var roleClaims = new List<Claim>();

                foreach (var name in user)
                {
                    userClaims.Add(new Claim(ClaimTypes.Name, name.TenQuyen));
                }

                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, firstUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, firstUser.Email),
                }
                .Union(userClaims)
                .Union(roleClaims);

                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                var jwtSecurityToken = new JwtSecurityToken
                (
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                    signingCredentials: signingCredentials
                );

                AuthResponse response = new AuthResponse
                {
                    Id = firstUser.MaNguoiDung.ToString(),
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    Email = firstUser.Email,
                    UserName = firstUser.UserName
                };

                return response;
            }
            else
            {
                throw new Exception($"User with {request.UserName} not found.");
            }
        }
    }
}