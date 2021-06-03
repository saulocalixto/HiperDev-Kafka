using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Ms_HistoricoClientes.Domain.Entidades
{
    public class HistoricoDeClientes
    {
        public string Acao { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public Guid IdCliente { get; set; }

        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public DateTime Timestamp { get; set; }
    }
}