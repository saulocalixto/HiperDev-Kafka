using CalixtosStore.Domain.Events;

namespace CalixtosStore.Domain.Producers
{
    public class HistoricoClienteProducer : Producer<HistoricoDeClienteEvent>
    {
        public HistoricoClienteProducer() : base("registrar-historico-cliente")
        {
        }
    }
}