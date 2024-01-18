using fastwin.Entities;
using fastwin.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace fastwin.Infrastructure.JWT
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;

        public AuthService(IConfiguration config, UserManager<User> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        public async Task<string> GenerateJWTString(LoginReq user)
{
    var appUser = await _userManager.FindByEmailAsync(user.Email);
    if (appUser == null)
    {
        // Kullanıcı bulunamadı.
        // İsterseniz burada hata fırlatabilir ya da loglayabilirsiniz.
        return null;
    }

    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.NameIdentifier, appUser.Id) // UserId'yi ekleyin
    };

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JwtTokenSettings:SecretKey").Value));

    SigningCredentials signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var securityToken = new JwtSecurityToken(
        claims: claims,
        expires: DateTime.Now.AddMinutes(60),
        issuer: _config.GetSection("JwtTokenSettings:ValidIssuer").Value,
        audience: _config.GetSection("JwtTokenSettings:ValidAudience").Value,
        signingCredentials: signingCred
    );

    string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
    return tokenString;
}
    }
}
