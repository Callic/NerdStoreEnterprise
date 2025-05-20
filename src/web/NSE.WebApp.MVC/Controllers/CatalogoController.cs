using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Services.Interfaces;

namespace NSE.WebApp.MVC.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ICatalogoService _catalogoService;

        public CatalogoController(ICatalogoService catalogoService)
        {
            _catalogoService = catalogoService;
        }

        [Route("")]
        [Route("vitrine")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var produtos = await _catalogoService.ObterProdutos();
            return View(produtos);
        }


        [Route("detalhes/{id}")]
        [HttpGet]
        public async Task <IActionResult> ProdutoDetalhe(Guid id)
        {
            if (id == Guid.Empty) return NotFound();
            var produto = await _catalogoService.ObterProdutoPorId(id);
            return View(id);
        }
    }
}
