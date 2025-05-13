using NSE.Core.Models.ViewModels;

namespace NSE.WebApp.MVC.Services
{
    public interface IAutenticacaoService
    {
        Task<Token> LoginAsync(UsuarioLogin usuarioLogin);
        Task<Token> RegistroAsync(UsuarioRegistro usuarioRegistro);
    }
}