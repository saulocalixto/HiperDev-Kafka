using Ms_HistoricoClientes.Data.Repositorios;

namespace Ms_HistoricoClientes.Services
{
    public class SegundoConsumer : ConsumerBase
    {
        public SegundoConsumer(HistoricoDeClientesRepositorio historicoRepositorio) : base(historicoRepositorio)
        {
        }
    }
}