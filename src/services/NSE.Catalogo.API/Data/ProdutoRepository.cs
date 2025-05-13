using Microsoft.EntityFrameworkCore;
using NSE.Catalogo.API.Data.Interfaces;
using NSE.Catalogo.API.Models;
using NSE.Core.Data;

namespace NSE.Catalogo.API.Data
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly CatalogoContext _catalogoContext;

        public ProdutoRepository(CatalogoContext catalogoContext)
        {
            _catalogoContext = catalogoContext;
        }

        public IUnitOfWork UnitOfWork => _catalogoContext;



        public async Task<List<Produto>> GetAllAsync()
        {
            return await _catalogoContext.Produto.ToListAsync();
        }

        public async Task<Produto?> GetAsync(Guid id)
        {
            return await _catalogoContext.Produto.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAsync(Produto entity)
        {
            await _catalogoContext.AddAsync(entity);
        }
        public void Delete(Produto entity)
        {
           _catalogoContext.Produto.Remove(entity);
        }

        public void Update(Produto entity)
        {
            _catalogoContext.Produto.Update(entity);
        }
    }
}
