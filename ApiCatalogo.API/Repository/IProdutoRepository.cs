using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        public IEnumerable<Produto> GetProdutoPorPreco();
        public IEnumerable<Produto> GetProdutoPorEstoque();
    }
}
