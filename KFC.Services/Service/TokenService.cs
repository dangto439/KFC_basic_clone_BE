using KFC.Contract.Repositories.Interface;
using KFC.Contract.Services.Interface;
using KFC.Core.Constants;
using KFC.Core.ExceptionCustom;
using KFC.Entity;
using KFC.ModelViews.ModelViews.UserModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KFC.Services.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public TokenService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public TokenResponse GenerateTokens(User user, string role)
        {
            DateTime now = DateTime.Now;

            // Common claims for both tokens
            List<Claim> claims = new List<Claim>
        {
            new Claim("id", user.Id.ToString()),
            new Claim("role", role),
            // assign 
            new Claim("exp", now.Ticks.ToString())
        };

            var keyString = _configuration.GetSection("JwtSettings:SecretKey").Value ?? string.Empty;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));

            var claimsIdentity = new ClaimsIdentity(claims, "Bearer");
            var principal = new ClaimsPrincipal(new[] { claimsIdentity });
            _httpContextAccessor.HttpContext.User = principal;

            Console.WriteLine("Check Key:", key);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            // Generate access token
            var accessToken = new JwtSecurityToken(
                claims: claims,
                issuer: _configuration.GetSection("JwtSettings:Issuer").Value,
                audience: _configuration.GetSection("JwtSettings:Audience").Value,
                expires: now.AddMinutes(30),
                signingCredentials: creds
            );
            var accessTokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);

            // Generate refresh token
            var refreshToken = new JwtSecurityToken(
                claims: claims,
                issuer: _configuration.GetSection("JwtSettings:Issuer").Value,
                audience: _configuration.GetSection("JwtSettings:Audience").Value,
                expires: now.AddDays(7),
                signingCredentials: creds
            );
            var refreshTokenString = new JwtSecurityTokenHandler().WriteToken(refreshToken);
            Role roleUser = _unitOfWork.GetRepository<Role>().Entities.Where(x => x.Id.ToString() == user.RoleId).FirstOrDefault()
                                    ?? throw new ErrorException(StatusCodes.Status401Unauthorized, ResponseCodeConstants.BADREQUEST, "Lỗi Authorize");
            string roleName = _unitOfWork.GetRepository<Role>().GetById(roleUser.Id).Name ?? "Unknow";
            // Return the tokens and user information
            return new TokenResponse
            {
                AccessToken = accessTokenString,
                RefreshToken = refreshTokenString,

                User = new ResponseUserModels
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password,
                    PhoneNumber = user.PhoneNumber,
                    RoleId = roleUser.Id.ToString(),
                    CreatedTime = user.CreatedTime,
                    CreatedBy = user.CreatedBy!
                }
            };
        }
    }
}
