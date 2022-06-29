using ApiCatalogo.Context;
using ApiCatalogo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalogoTests.UseCaseTests
{
    public class DBUnitTestsMockInitializer
    {
        public DBUnitTestsMockInitializer()
        {

        }

        public void Seed(AppDbContext context)
        {
            context.Produtos.Add
        (new Produto { ProdutoId = 1, Nome = "suco", Descricao = "Descrição", Preco = 1, ImagemUrl = "suco.jpg", CategoriaId = 1 });
         
            context.Produtos.Add
         (new Produto { ProdutoId = 1, Nome = "teste", Descricao = "Descrição teste", Preco = 2, ImagemUrl = "teste.jpg", CategoriaId = 2 });

            context.Produtos.Add
       (new Produto { ProdutoId = 1, Nome = "teste2", Descricao = "Descrição teste 2", Preco = 3, ImagemUrl = "teste2.jpg", CategoriaId = 3 });
        }
    }
}
