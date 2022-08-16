namespace ApiCatalogo.Repository
{
    public interface IUnitOfWork
    {
        public ICategoriaRepository CategoriaRepository { get; }
        public IProdutoRepository ProdutoRepository { get; }
        public void Commit();
    }
}
