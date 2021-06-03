using MongoDB.Driver;
using Ms_HistoricoClientes.Data.Configuracoes;
using Ms_HistoricoClientes.Domain.Entidades;
using System.Collections.Generic;

namespace Ms_HistoricoClientes.Data.Repositorios
{
    public class HistoricoDeClientesRepositorio
    {
        private readonly IMongoCollection<HistoricoDeClientes> _historicoClientes;

        public HistoricoDeClientesRepositorio(IHistoricoClientesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _historicoClientes = database.GetCollection<HistoricoDeClientes>(settings.BooksCollectionName);
        }

        public List<HistoricoDeClientes> Get() =>
            _historicoClientes.Find(book => true).ToList();

        public HistoricoDeClientes Get(string id) =>
            _historicoClientes.Find<HistoricoDeClientes>(book => book.Id == id).FirstOrDefault();

        public HistoricoDeClientes Create(HistoricoDeClientes book)
        {
            _historicoClientes.InsertOne(book);
            return book;
        }

        public void Update(string id, HistoricoDeClientes bookIn) =>
            _historicoClientes.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(HistoricoDeClientes bookIn) =>
            _historicoClientes.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) =>
            _historicoClientes.DeleteOne(book => book.Id == id);
    }
}