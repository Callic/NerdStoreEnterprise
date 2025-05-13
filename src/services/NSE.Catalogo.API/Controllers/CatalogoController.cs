using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Catalogo.API.Data.Interfaces;
using NSE.Catalogo.API.Models;
using NSE.WebAPI.Core.Identidade;

namespace NSE.Catalogo.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public CatalogoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [AllowAnonymous]
        [HttpGet("produtos")]
        public async Task<IActionResult> Index()
        { 
            return Ok(await _produtoRepository.GetAllAsync());
        }

        [ClaimsAuthorize("Catalogo", "Ler")]
        [HttpGet("produto/{id}")]
        public async Task<IActionResult> GetProduto(Guid id)
        {
            var produto = await _produtoRepository.GetAsync(id);
            
            if(produto == null)
                return NotFound();

            return Ok(produto);
        }

        [ClaimsAuthorize("Catalogo", "Escrever")]
        [HttpPost("produto")]
        public async Task<IActionResult> Create(Produto produto)
        {
            await _produtoRepository.CreateAsync(produto);
            
            if(await _produtoRepository.UnitOfWork.Commit())
                return Created();

            return BadRequest();
        }

        [ClaimsAuthorize("Catalogo", "Escrever")]
        [HttpPatch("produto/{id}")]
        public async Task<IActionResult> Update(Guid id, Produto produto)
        {
            var prod = await _produtoRepository.GetAsync(id);
            if(prod == null)
                return NotFound();

            produto.Id = id;
            _produtoRepository.Update(produto);
            if(await _produtoRepository.UnitOfWork.Commit())
                return NoContent();

            return BadRequest();

        }

        [ClaimsAuthorize("Catalogo", "Escrever")]
        [HttpDelete("produto/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var prod = await _produtoRepository.GetAsync(id);
            if (prod == null)
                return NotFound();

            _produtoRepository.Delete(prod);
            if (await _produtoRepository.UnitOfWork.Commit())
                return NoContent();

            return BadRequest();
        }
    }
}
