using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using TicketingSystem.Application.Abstractions.IRepositories;
using TicketingSystem.Application.Abstractions.IServices;
using TicketingSystem.Domain.Entities.Models;

namespace TicketingSystem.Application.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepo;

        public AuthService(IConfiguration configuration, IUserRepository userRepo)
        {
            _userRepo = userRepo;
            _configuration = configuration;
        }

        public async Task<ResponceLogin> GenerateToken(RequestLogin request)
        {
            if (request == null)
            {
                return new ResponceLogin()
                {
                    Token = "User Not Found"
                };
            }
            var findUser = await FindUser(request);
            if (findUser != null)
            {

                var permission = new List<int>();

                if (findUser.role != "Admin")
                {
                    permission = new List<int> { 1, 2, 3, 4, 5, 8, 9, 10 };
                }
                else
                {
                    permission = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };


                }
                var jsonContent = JsonSerializer.Serialize(permission);
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Role, findUser.role),
                    new Claim("Login", findUser.Login),
                    new Claim("UserID", findUser.Id.ToString()),
                    new Claim("CreatedDate", DateTime.UtcNow.ToString()),
                    new Claim("Permissions", jsonContent)
                };

                return await GenerateToken(claims);
            }

            return new ResponceLogin()
            {
                Token = "Un Authorize"
            };
        }

        public async Task<ResponceLogin> GenerateToken(IEnumerable<Claim> additionalClaims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var exDate = Convert.ToInt32(_configuration["JWT:ExpireDate"] ?? "10");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(DateTime.UtcNow).ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64)
            };

            if (additionalClaims?.Any() == true)
                claims.AddRange(additionalClaims);


            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(exDate),
                signingCredentials: credentials);

            return new ResponceLogin()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };


        }
        public async Task<User> FindUser(RequestLogin user)
        {

            var result = await _userRepo.GetByAny(x => x.Login == user.Login);

            if (user.Login == result.Login && user.Password == result.Password)
            {
                return result;
            }

            return new User();
        }
    }
}
