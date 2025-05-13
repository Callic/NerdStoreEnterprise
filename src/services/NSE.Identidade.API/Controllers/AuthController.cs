using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSE.Identidade.API.Interfaces;
using NSE.Core.Models.ViewModels;
using System.Net;

namespace NSE.Identidade.API.Controllers
{
    [Route("api/[controller]")]

    public class AuthController : MainController
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IJwtService _jwtService;

        public AuthController(UserManager<IdentityUser> userManager,
                              SignInManager<IdentityUser> signInManager,
                              IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarUsuario(UsuarioRegistro usuario)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {

                Email = usuario.Email,
                EmailConfirmed = true,
                UserName = usuario.Email
            };

            var result = await _userManager.CreateAsync(user, usuario.Senha!);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    AdicionarErro(error.Description);
                }
                return CustomResponse(HttpStatusCode.BadRequest);
            }

            return CustomResponse(HttpStatusCode.Created);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UsuarioLogin usuario)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _signInManager.PasswordSignInAsync(usuario.Email!, usuario.Senha!, false, true);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    AdicionarErro("Usuário temporariamente bloqueado por tentavas inválidas");
                else
                    AdicionarErro("Usuário ou senha incorreto.");

                return CustomResponse(HttpStatusCode.BadRequest);
            }

            var user = await _userManager.FindByEmailAsync(usuario.Email!);
            return CustomResponse(HttpStatusCode.OK, await _jwtService.CreateJwtToken(user!));
        }
    }
}
