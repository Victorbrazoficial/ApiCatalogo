using ApiCatalogo.Context;
using ApiCatalogo.Controllers.Produtos;
using ApiCatalogo.UseCase;
using ApiCatalogo.UseCase.InterfaceUseCase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalogoTests.UseCaseTests
{
    public class ListarProdutosUseCaseTests
    {
        private readonly AppDbContext _context;
        //private IListarProdutosUseCase _listarProdutosUseCase;
        //public ListarProdutosUseCaseTests(AppDbContext context)
        //{
        //    _context = context;
        //}

        public static DbContextOptions<AppDbContext> dbContextOptions { get; }

        public static string connectionString = "Server=localhost;DataBase=ApiCatalogoDB;Uid=victor;Pwd=97317089";

        static ListarProdutosUseCaseTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connectionString, )
                .Options;
               
        }

        [Fact]
        public void testa()
        {
            var a = new ListarProdutosUseCase(_context);
            var result = a.Execute();

            //var resut = UseCase.Execute();

            //Assert.NotNull(resut);

            Assert.Equal(3,result.Count());
        }
    }
}
