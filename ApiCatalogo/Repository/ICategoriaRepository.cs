using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
       public PagedList<Categoria> GetCategorias(CategoriaParameters categoriaParameters);
       public PagedList<Categoria> GetCategoriasProdutos(CategoriaParameters categoriaParameters);
    }
}
