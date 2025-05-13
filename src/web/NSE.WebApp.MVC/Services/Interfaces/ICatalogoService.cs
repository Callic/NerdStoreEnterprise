using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services.Interfaces
{
    public interface ICatalogoService
    {
        Task<IEnumerable<ProdutoViewModel>> ObterProdutos();
        Task<ProdutoViewModel> ObterProdutoPorId(Guid id);
    }
}
