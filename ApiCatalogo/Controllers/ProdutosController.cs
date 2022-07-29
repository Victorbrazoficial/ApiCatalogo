using ApiCatalogo.Context;
using ApiCatalogo.Filters;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ProdutosController> _logger;

        public ProdutosController(AppDbContext context, ILogger<ProdutosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Produto>> GetListaDeProdutos()
        {
            try
            {
                var produtos = _context.Produtos.Take(10).AsNoTracking().ToList();

                if (produtos is null)
                    return NotFound("Produtos não encontrado");

                _logger.LogInformation("{class} - {method} - Request '{@request}'",
                   nameof(ProdutosController),
                   nameof(ProdutosController.GetListaDeProdutos),
                   "none");

                return produtos;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> GetProdutosPorId(int id)
        {
            try
            {
                var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

                if (produto is null)
                    return NotFound("Produto não encontrado");

                 _logger.LogInformation("{class} - {method} - Request '{@request}'",
                    nameof(ProdutosController),
                    nameof(ProdutosController.GetProdutosPorId),
                    id);

                return produto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpPost]
        public ActionResult PostProduto(Produto produto)
        {
            try
            {
                if (produto is null)
                    return BadRequest();

                _context.Produtos.Add(produto);
                _context.SaveChanges();

                _logger.LogInformation("{class} - {method} - Request '{@request}'",
                   nameof(ProdutosController),
                   nameof(ProdutosController.PostProduto),
                   produto);

                return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult PutProduto(int id, Produto produto)
        {
            try
            {
                if (id != produto.ProdutoId)
                    return BadRequest();

                _context.Entry(produto).State = EntityState.Modified;
                _context.SaveChanges();

                _logger.LogInformation("{class} - {method} - Request '{@request} - Request 2 {@request}'",
                   nameof(ProdutosController),
                   nameof(ProdutosController.PutProduto),
                   id, produto);

                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteProduto(int id)
        {
            try 
            {
                var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

                if (produto is null)
                    return NotFound("Produto não encontrado");

                _context.Produtos.Remove(produto);
                _context.SaveChanges();

                _logger.LogInformation("{class} - {method} - Request '{@request}'",
                   nameof(ProdutosController),
                   nameof(ProdutosController.DeleteProduto),
                   id);

                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }
    }
}
