using System;

namespace CalixtosStore.Email.Cliente
{
    public class NovoCliente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public DateTime Timestamp { get; set; }
        public string MessageType { get; set; }
        public Guid AggregateId { get; set; }
    }
}