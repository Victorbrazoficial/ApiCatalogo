using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route ("[Controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("Produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            var categoriasProdutos = _context.Categorias.Include(p => p.Produtos).ToList();

            return Ok(categoriasProdutos);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> GetCategorias()
        {
            var categorias = _context.Categorias.ToList();

            if (categorias is null)
                return NotFound("Categorias não encontrados");

            return Ok(categorias);
        }

        [HttpGet("{id:int}", Name = "ObeterCategoria")]
        public ActionResult<Categoria> GetCategoriaPorId(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            
            if (categoria is null)
                return NotFound("Categoria não encontrado.");

            return categoria;
        }

        [HttpPost]
        public ActionResult PostCategoria(Categoria categoria)
        {
            if (categoria is null)
                return BadRequest("Categoria null");

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObeterCategoria", new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public ActionResult PutCategoria(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
                return BadRequest();

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteCategoria(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

            if (categoria is null)
                return NotFound("Categoria não encontrada");

            _context.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }
    }
}
