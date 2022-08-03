using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        public PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters);
        public PagedList<Produto> GetProdutoPorPreco(ProdutosParameters produtosParameters);
    }
}
