using System;
using System.Collections.Generic;
using System.Text;

namespace Ms_HistoricoClientes.Data.Configuracoes
{
    public class HistoricoClientesDatabaseSettings : IHistoricoClientesDatabaseSettings
    {
        public string BooksCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}