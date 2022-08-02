using ApiCatalogo.Context;
using ApiCatalogo.Models;

namespace ApiCatalogo.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Produto> GetProdutoPorNome()
        {
            return Get().OrderBy(p => p.Nome).ToList();
        }

        public IEnumerable<Produto> GetProdutoPorPreco()
        {
            return Get().OrderBy(p => p.Preco).ToList();
        }
    }
}
