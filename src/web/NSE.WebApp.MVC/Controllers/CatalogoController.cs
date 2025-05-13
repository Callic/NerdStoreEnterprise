using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Services.Interfaces;
using System.Threading.Tasks;

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
        public IActionResult ProdutoDetalhe(Guid id)
        {
            if (id == Guid.Empty) return NotFound();
            return View(id);
        }
    }
}
