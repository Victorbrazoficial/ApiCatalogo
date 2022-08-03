using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Produto> GetProdutoPorPreco(ProdutosParameters produtosParameters)
        {
            return Get()
                .OrderBy(p => p.Preco)
                .Skip((produtosParameters.PageNumber -1) * produtosParameters.PageSize)
                .Take(produtosParameters.PageSize)
                .ToList();
        }

        public PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters)
        {
            return PagedList<Produto>.ToPagedList(
                Get().OrderBy(on => on.Nome),
                produtosParameters.PageNumber, 
                produtosParameters.PageSize
                );
        }
    }
}
