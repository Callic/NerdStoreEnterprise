using Microsoft.AspNetCore.Identity;
using NSE.Core.Models.ViewModels;

namespace NSE.Identidade.API.Interfaces
{
    public interface IJwtService
    {
        Task<UsuarioRespostaLogin> CreateJwtToken(IdentityUser user);
    }
}