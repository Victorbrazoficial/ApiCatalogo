using ApiCatalogo.Context;
using ApiCatalogo.DTOs;
using ApiCatalogo.Filters;
using ApiCatalogo.Models;
using ApiCatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly ILogger<ProdutosController> _logger;
        private readonly IMapper _mapper;

        public ProdutosController(IUnitOfWork uof, ILogger<ProdutosController> logger, IMapper mapper)
        {
            _uof = uof;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<ProdutoDTO>> GetListaDeProdutos()
        {
            try
            {
                var produtos = _uof.ProdutoRepository.Get().ToList();

                if (produtos is null)
                    return NotFound("Produtos não encontrado");

                _logger.LogInformation("{class} - {method} - Request '{@request}'",
                   nameof(ProdutosController),
                   nameof(ProdutosController.GetListaDeProdutos),
                   "none");

                var produtosDTO = _mapper.Map<List<ProdutoDTO>>(produtos);
               
                return produtosDTO;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<ProdutoDTO> GetProdutosPorId(int id)
        {
            try
            {
                var produto = _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);

                if (produto is null)
                    return NotFound("Produto não encontrado");

                 _logger.LogInformation("{class} - {method} - Request '{@request}'",
                    nameof(ProdutosController),
                    nameof(ProdutosController.GetProdutosPorId),
                    id);

                var produtoDTO = _mapper.Map<ProdutoDTO>(produto);
                
                return produtoDTO;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpGet("menorpreco")]
        public ActionResult<ProdutoDTO> GetProdutosPreco()
        {
            try
            {
                var produto = _uof.ProdutoRepository.GetProdutoPorPreco();

                if (produto is null)
                    return NotFound("Produto não encontrado");

                _logger.LogInformation("{class} - {method} - Request '{@request}'",
                   nameof(ProdutosController),
                   nameof(ProdutosController.GetProdutosPreco), 
                   "none");

                var produtosDTO = _mapper.Map<List<ProdutoDTO>>(produto);
                return Ok(produtosDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpGet("ordemalfabetica")]
        public ActionResult<Produto> GetProdutosNome()
        {
            try
            {
                var produto = _uof.ProdutoRepository.GetProdutoPorNome();

                if (produto is null)
                    return NotFound("Produto não encontrado");

                _logger.LogInformation("{class} - {method} - Request '{@request}'",
                   nameof(ProdutosController),
                   nameof(ProdutosController.GetProdutosNome),
                   "none");

                var produtosDTO = _mapper.Map<List<ProdutoDTO>>(produto);
                return Ok(produtosDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpPost]
        public ActionResult PostProduto(ProdutoDTO produtoDTO)
        {
            try
            {
                if (produtoDTO is null)
                    return BadRequest();

                var produto = _mapper.Map<Produto>(produtoDTO);

                _uof.ProdutoRepository.Add(produto);
                _uof.Commit();

                _logger.LogInformation("{class} - {method} - Request '{@request}'",
                   nameof(ProdutosController),
                   nameof(ProdutosController.PostProduto),
                   produtoDTO);

                return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produtoDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult PutProduto(int id, ProdutoDTO produtoDTO)
        {
            try
            {
                if (id != produtoDTO.ProdutoId)
                    return BadRequest();

                var produto = _mapper.Map<Produto>(produtoDTO);

                _uof.ProdutoRepository.Update(produto);
                _uof.Commit();

                _logger.LogInformation("{class} - {method} - Request '{@request} - Request 2 {@request}'",
                   nameof(ProdutosController),
                   nameof(ProdutosController.PutProduto),
                   id, produtoDTO);

                return Ok(produtoDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ProdutoDTO> DeleteProduto(int id)
        {
            try 
            {
                var produto = _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);

                if (produto is null)
                    return NotFound("Produto não encontrado");

                _uof.ProdutoRepository.Delete(produto);
                _uof.Commit();

                _logger.LogInformation("{class} - {method} - Request '{@request}'",
                   nameof(ProdutosController),
                   nameof(ProdutosController.DeleteProduto),
                   id);

                var produtoDTO = _mapper.Map<ProdutoDTO>(produto);
                
                return produtoDTO;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }
    }
}
