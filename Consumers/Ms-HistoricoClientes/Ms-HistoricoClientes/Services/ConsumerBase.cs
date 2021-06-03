using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Ms_HistoricoClientes.Data.Repositorios;
using Ms_HistoricoClientes.Domain.Entidades;
using Newtonsoft.Json;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ms_HistoricoClientes.Services
{
    public abstract class ConsumerBase : BackgroundService
    {
        private readonly HistoricoDeClientesRepositorio _historicoRepositorio;
        private const string _nomeTopico = "registrar-historico-cliente";
        private const string _grupoId = "HiperDevHistorico";
        private const string _bootstrapServer = "localhost:9092";

        protected ConsumerBase(HistoricoDeClientesRepositorio historicoRepositorio)
        {
            _historicoRepositorio = historicoRepositorio;
        }

        private ConsumerConfig ObterConfiguracoes(string bootstrapServers, string groupId)
        {
            return new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            var logger = new LoggerConfiguration()
                .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
                .CreateLogger();
            logger.Information("Testando o consumo de mensagens com Kafka");

            logger.Information($"BootstrapServers = {_bootstrapServer}");
            logger.Information($"Topic = {_nomeTopico}");
            logger.Information($"Group Id = {_grupoId}");

            var config = ObterConfiguracoes(_bootstrapServer, _grupoId);

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe(_nomeTopico);
                ExecutarConsumo(logger, consumer, stoppingToken);
            }
        }

        private void ExecutarConsumo(Serilog.Core.Logger logger, IConsumer<Ignore, string> consumer, CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var cr = consumer.Consume(stoppingToken);

                        if (!cr.IsPartitionEOF)
                        {
                            var mensagem = cr.Message.Value;

                            var historico = JsonConvert.DeserializeObject<HistoricoDeClientes>(mensagem);

                            _historicoRepositorio.Create(historico);

                            logger.Information($"Mensagem lida: {cr.Message.Value}");
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Error($"Algo deu errado: {e.Message}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
                logger.Warning("Cancelada a execução do Consumer...");
            }
        }
    }
}