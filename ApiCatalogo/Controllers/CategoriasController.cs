using ApiCatalogo.Models;
using ApiCatalogo.Repository;
using ApiCatalogo.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers
{
    [Route ("[Controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {

        private readonly IUnitOfWork _uof;
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(IUnitOfWork uof, ILogger<CategoriasController> logger)
        {
            _uof = uof;
            _logger = logger;  
        }

        [HttpGet("saudacoes/{nome}")]
        public ActionResult<string> GetServico([FromServices] IMeuServico meuServico, string nome)
        {
            return meuServico.Saudacao(nome);
        }

        [HttpGet("Produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            try
            {
                var categoriasProdutos = _uof.CategoriaRepository.GetCategoriasProdutos();

                _logger.LogInformation("{class} - {method} - Request '{@request}'",
                   nameof(CategoriasController),
                   nameof(CategoriasController.GetCategoriasProdutos),
                   "none");

                return Ok(categoriasProdutos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> GetCategorias()
        {
            try
            {
                var categorias = _uof.CategoriaRepository.Get().ToList();

                if (categorias is null)
                    return NotFound("Categorias não encontradas");

                _logger.LogInformation("{class} - {method} - Request '{@request}'",
                   nameof(CategoriasController),
                   nameof(CategoriasController.GetCategorias),
                   "none");

                return Ok(categorias);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpGet("{id:int}", Name = "ObeterCategoria")]
        public ActionResult<Categoria> GetCategoriaPorId(int id)
        {
            try
            {
                var categoria = _uof.CategoriaRepository.GetById(c => c.CategoriaId == id);
          
                if (categoria is null)
                    return NotFound("Categoria não encontrada.");

                _logger.LogInformation("{class} - {method} - Request '{@request}'",
                   nameof(CategoriasController),
                   nameof(CategoriasController.GetCategoriaPorId),
                   id);

                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpPost]
        public ActionResult PostCategoria(Categoria categoria)
        {
            try 
            {
                if (categoria is null)
                    return BadRequest("Categoria não encontrada.");

                _uof.CategoriaRepository.Add(categoria);
                _uof.Commit();

                _logger.LogInformation("{class} - {method} - Request '{@request}'",
                   nameof(CategoriasController),
                   nameof(CategoriasController.PostCategoria),
                   categoria);

                return new CreatedAtRouteResult("ObeterCategoria", new { id = categoria.CategoriaId }, categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult PutCategoria(int id, Categoria categoria)
        {
            try
            {
                if (id != categoria.CategoriaId)
                    return BadRequest("Categoria não encontrada.");

                _uof.CategoriaRepository.Update(categoria);
                _uof.Commit();

                _logger.LogInformation("{class} - {method} - Request '{@request} - Request 2 {@request}'",
                   nameof(CategoriasController),
                   nameof(CategoriasController.PutCategoria),
                   id, categoria);

                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteCategoria(int id)
        {
            try 
            {
                var categoria = _uof.CategoriaRepository.GetById(c => c.CategoriaId == id);

                if (categoria is null)
                    return NotFound("Categoria não encontrada");

                _uof.CategoriaRepository.Delete(categoria);
                _uof.Commit();

                _logger.LogInformation("{class} - {method} - Request '{@request}'",
                   nameof(CategoriasController),
                   nameof(CategoriasController.PostCategoria),
                   id);

                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }
    }
}
