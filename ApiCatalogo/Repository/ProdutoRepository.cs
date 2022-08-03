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

        public PagedList<Produto> GetProdutoPorPreco(ProdutosParameters produtosParameters)
        {
            return PagedList<Produto>.ToPagedList(
                Get().OrderBy(p => p.Preco),
                produtosParameters.PageNumber,
                produtosParameters.PageSize
            );
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
