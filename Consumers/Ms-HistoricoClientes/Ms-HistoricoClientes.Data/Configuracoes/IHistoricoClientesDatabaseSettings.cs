namespace Ms_HistoricoClientes.Data.Configuracoes
{
    public interface IHistoricoClientesDatabaseSettings
    {
        string BooksCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}