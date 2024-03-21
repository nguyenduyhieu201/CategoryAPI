using CategoryAPI.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CategoryAPI.Services
{
    public class AuthenService : IAuthenService
    {
        private IConfiguration _configuration;
        public AuthenService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public string username = "duyhieu948";
        public string password = "duyhieu948";

        public LoginResponseModel Authenticate(LoginModel loginModel)
        {
            if(!loginModel.username.Equals(username)  || !loginModel.password.Equals(password))
            {
                return new LoginResponseModel
                {
                    Token = null,
                    Message = "Username or password failed",
                    Success = false
                };
            }

            return new LoginResponseModel
            {
                Token = GenerateToken(loginModel.username),
                Message = "Login success",
                Success = true
            };
        }

        public string GenerateToken(string username)
        {
            var secretKey = _configuration["JwtConfig:secretKey"];
            var issuer = _configuration["JwtConfig:validIssuer"];
            var audience = _configuration["JwtConfig:validAudience"];
            var expiresIn = Convert.ToInt32(_configuration["JwtConfig:expiresIn"]);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, username)
            // Các claim khác có thể thêm vào đây
        };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresIn),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
