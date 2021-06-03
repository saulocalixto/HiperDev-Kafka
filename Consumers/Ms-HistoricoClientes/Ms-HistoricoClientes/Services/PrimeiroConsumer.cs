using Ms_HistoricoClientes.Data.Repositorios;

namespace Ms_HistoricoClientes.Services
{
    public class PrimeiroConsumer : ConsumerBase
    {
        public PrimeiroConsumer(HistoricoDeClientesRepositorio historicoRepositorio) : base(historicoRepositorio)
        {
        }
    }
}