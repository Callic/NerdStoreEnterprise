using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services.Interfaces;

namespace NSE.WebApp.MVC.Services
{
    public class CatalogoService : Service, ICatalogoService
    {
        private readonly HttpClient _httpClient;
        
        public CatalogoService(HttpClient httpClient, AppSettings appSettings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(appSettings!.CatalogoUrl);
        }


        public async Task<IEnumerable<ProdutoViewModel>> ObterProdutos()
        {
            var response = await _httpClient.GetAsync("api/catalogo/produtos");
            TratarRespostaErro(response);
            return await DeserializarObjetoResponse<IEnumerable<ProdutoViewModel>>(response);
        }

        public async Task<ProdutoViewModel> ObterProdutoPorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/catalogo/produtos/{id}");
            TratarRespostaErro(response);
            return await DeserializarObjetoResponse<ProdutoViewModel>(response);
        }
    }
}
