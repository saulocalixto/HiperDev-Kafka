using CalixtosStore.Domain.Core.Events;
using System;

namespace CalixtosStore.Domain.Events
{
    public class HistoricoDeClienteEvent : Evento
    {
        public HistoricoDeClienteEvent(Guid id, string acao, string nome, string email, DateTime dataNascimento)
        {
            IdCliente = id;
            Acao = acao;
            Nome = nome;
            Email = email;
            DataDeNascimento = dataNascimento;
        }

        public string Acao { get; set; }
        public Guid IdCliente { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataDeNascimento { get; set; }
    }
}