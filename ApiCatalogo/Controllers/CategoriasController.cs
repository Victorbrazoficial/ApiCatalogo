using ApiCatalogo.DTOs;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using ApiCatalogo.Repository;
using ApiCatalogo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiCatalogo.Controllers
{
    [Route ("[Controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {

        private readonly IUnitOfWork _uof;
        private readonly ILogger<CategoriasController> _logger;
        private readonly IMapper _mapper;

        public CategoriasController(IUnitOfWork uof, ILogger<CategoriasController> logger, IMapper mapper)
        {
            _uof = uof;
            _logger = logger;  
            _mapper = mapper;
        }

        [HttpGet("saudacoes/{nome}")]
        public ActionResult<string> GetServico([FromServices] IMeuServico meuServico, string nome)
        {
            return meuServico.Saudacao(nome);
        }

        [HttpGet("Produtos")]
        public ActionResult<IEnumerable<CategoriaDTO>> GetCategoriasProdutos([FromQuery] CategoriaParameters categoriaParameters)
        {
            try
            {
                var categoriasProdutos = _uof.CategoriaRepository.GetCategoriasProdutos(categoriaParameters);

                var metadata = new 
                {
                    categoriasProdutos.TotalCount,
                    categoriasProdutos.PageSize,
                    categoriasProdutos.CurrentPage,
                    categoriasProdutos.TotalPages,
                    categoriasProdutos.HasNext,
                    categoriasProdutos.HasPrevious
                };

                _logger.LogInformation("{class} - {method} - Request '{@request}'",
                   nameof(CategoriasController),
                   nameof(CategoriasController.GetCategoriasProdutos),
                   "none");

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                var categoriasProdutosDTO = _mapper.Map<IEnumerable<CategoriaDTO>>(categoriasProdutos);

                return Ok(categoriasProdutosDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoriaDTO>> GetCategorias([FromQuery] CategoriaParameters categoriaParameters)
        {
            try
            {
                var categorias = _uof.CategoriaRepository.GetCategorias(categoriaParameters);

                var metadata = new 
                {
                    categorias.TotalCount,
                    categorias.PageSize,
                    categorias.CurrentPage,
                    categorias.TotalPages,
                    categorias.HasNext,
                    categorias.HasPrevious
                };

                if (categorias is null)
                    return NotFound("Categorias não encontradas");

                _logger.LogInformation("{class} - {method} - Request '{@request}'",
                   nameof(CategoriasController),
                   nameof(CategoriasController.GetCategorias),
                   "none");

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata)); 

                var categoriasDTO = _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);

                return Ok(categoriasDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpGet("{id:int}", Name = "ObeterCategoria")]
        public ActionResult<CategoriaDTO> GetCategoriaPorId(int id)
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
                
                var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

                return Ok(categoriaDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpPost]
        public ActionResult PostCategoria(CategoriaDTO categoriaDTO)
        {
            try 
            {
                if (categoriaDTO is null)
                    return BadRequest("Categoria não encontrada.");

                var categoria = _mapper.Map<Categoria>(categoriaDTO);

                _uof.CategoriaRepository.Add(categoria);
                _uof.Commit();

                _logger.LogInformation("{class} - {method} - Request '{@request}'",
                   nameof(CategoriasController),
                   nameof(CategoriasController.PostCategoria),
                   categoriaDTO);

                return new CreatedAtRouteResult("ObeterCategoria", new { id = categoria.CategoriaId }, categoriaDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult PutCategoria(int id, CategoriaDTO categoriaDTO)
        {
            try
            {
                if (id != categoriaDTO.CategoriaId)
                    return BadRequest("Categoria não encontrada.");

                var categoria = _mapper.Map<Categoria>(categoriaDTO);

                _uof.CategoriaRepository.Update(categoria);
                _uof.Commit();

                _logger.LogInformation("{class} - {method} - Request '{@request} - Request 2 {@request}'",
                   nameof(CategoriasController),
                   nameof(CategoriasController.PutCategoria),
                   id, categoriaDTO);

                return Ok(categoriaDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult<CategoriaDTO> DeleteCategoria(int id)
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

                var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

                return Ok(categoriaDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }
    }
}
