using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NSE.Core.Models.ViewModels;
using NSE.Identidade.API.Interfaces;
using NSE.WebAPI.Core.Identidade;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NSE.Identidade.API.Services 
{
    public class JwtService : IJwtService
    {
        private readonly AppSettings _appSettings;
        private readonly UserManager<IdentityUser> _userManager;
        public JwtService(IConfiguration configuration,
                           UserManager<IdentityUser> userManager)
        {
            _appSettings = new AppSettings();
            configuration.Bind("AppSettings", _appSettings);
            _userManager = userManager;
        }

        public async Task<UsuarioRespostaLogin> CreateJwtToken(IdentityUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var date = DateTime.UtcNow;

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email!));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, date.ToString()));
            foreach (var item in roles)
            {
                claims.Add(new Claim("role", item));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JwtSettings.Secret!);

            var hrsExpire = Convert.ToDouble(_appSettings.JwtSettings.ExpiracaoHoras);
            var expire = date.AddHours(hrsExpire);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.JwtSettings.Emissor,
                Audience = _appSettings.JwtSettings.ValidoEm,
                Subject = identityClaims,
                Expires = expire,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new UsuarioRespostaLogin
            {
                AcessToken = encodedToken,
                ExpireIn = TimeSpan.FromHours(Convert.ToDouble(_appSettings.JwtSettings.ExpiracaoHoras)).TotalSeconds,
                UsuarioToken = new UsuarioToken
                {
                    Id = user.Id,
                    Email = user.Email!,
                    Claims = claims.Select(x => new UsuarioClaim { Type = x.Type, Value = x.Value })
                }

            };

            return response;
        }


    }
}
