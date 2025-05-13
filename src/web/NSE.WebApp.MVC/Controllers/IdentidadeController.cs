using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NSE.Core.Models.ViewModels;
using NSE.WebApp.MVC.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NSE.WebApp.MVC.Controllers
{
    public class IdentidadeController : MainController
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public IdentidadeController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [HttpGet("nova-conta")]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost("nova-conta")]
        public async Task<IActionResult> Registro(UsuarioRegistro usuario)
        {
            if (!ModelState.IsValid) return View(usuario);

            var result = await _autenticacaoService.RegistroAsync(usuario);

            await RealizarLogin(result);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("login")]
        public IActionResult Login(string? ReturnUrl = null)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UsuarioLogin usuario, string? ReturnUrl = null)
        {
            if (!ModelState.IsValid) return View(usuario);

            var result = await _autenticacaoService.LoginAsync(usuario);


            if (PossuiErros(result.ResponseResult))
                return View(usuario);

            await RealizarLogin(result);

            if (string.IsNullOrEmpty(ReturnUrl)) return RedirectToAction("Index", "Home");

            return LocalRedirect(ReturnUrl);

        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("login");
        }

        private async Task RealizarLogin(Token token)
        {
            var tokenFormatado = ObterTokenFormatado(token.AcessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", token.AcessToken));
            claims.AddRange(tokenFormatado.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
                );

        }

        private static JwtSecurityToken ObterTokenFormatado(string token)
        {
            return new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
        }
    }
}
