using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<Categoria> GetCategorias(CategoriaParameters categoriaParameters)
        {
            return PagedList<Categoria>.ToPagedList(
                Get().OrderBy(c => c.Nome),
                categoriaParameters.PageNumber,
                categoriaParameters.PageSize   
            );
        }

        public PagedList<Categoria> GetCategoriasProdutos(CategoriaParameters categoriaParameters)
        {
            return PagedList<Categoria>.ToPagedList(
                Get().Include(x => x.Produtos),
                categoriaParameters.PageNumber,
                categoriaParameters.PageSize
            );
        }
    }
}
