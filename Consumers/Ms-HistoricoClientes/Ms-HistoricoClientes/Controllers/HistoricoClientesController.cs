using Microsoft.AspNetCore.Mvc;
using Ms_HistoricoClientes.Data.Repositorios;
using Ms_HistoricoClientes.Domain.Entidades;
using System.Collections.Generic;

namespace Ms_HistoricoClientes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HistoricoClientesController : ControllerBase
    {
        private readonly HistoricoDeClientesRepositorio _historicoRepositorio;

        public HistoricoClientesController(HistoricoDeClientesRepositorio historicoRepositorio)
        {
            _historicoRepositorio = historicoRepositorio;
        }

        [HttpGet]
        public IEnumerable<HistoricoDeClientes> Get()
        {
            return _historicoRepositorio.Get();
        }
    }
}