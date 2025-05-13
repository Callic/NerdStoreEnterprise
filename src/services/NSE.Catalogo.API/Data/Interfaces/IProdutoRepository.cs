using NSE.Catalogo.API.Models;
using NSE.Core.Data;

namespace NSE.Catalogo.API.Data.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        public IUnitOfWork UnitOfWork { get; }
    }
}
