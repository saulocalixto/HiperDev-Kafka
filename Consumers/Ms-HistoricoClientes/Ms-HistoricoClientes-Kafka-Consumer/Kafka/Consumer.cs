using Confluent.Kafka;
using Ms_HistoricoClientes.Data.Repositorios;
using Ms_HistoricoClientes.Domain.Entidades;
using Newtonsoft.Json;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Threading;

namespace Ms_HistoricoClientes_Kafka_Consumer.Kafka
{
    public class Consumer
    {
        private readonly HistoricoDeClientesRepositorio _historicoRepositorio;
        private const string _nomeTopico = "registrar-historico-cliente";
        private const string _grupoId = "HiperDev";
        private const string _bootstrapServer = "localhost:9092";

        public Consumer(HistoricoDeClientesRepositorio historicoRepositorio)
        {
            _historicoRepositorio = historicoRepositorio;
        }

        public void Consumir()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
                .CreateLogger();
            logger.Information("Testando o consumo de mensagens com Kafka");

            logger.Information($"BootstrapServers = {_bootstrapServer}");
            logger.Information($"Topic = {_nomeTopico}");
            logger.Information($"Group Id = {_grupoId}");

            var config = ObterConfiguracoes(_bootstrapServer, _grupoId);

            var cts = new CancellationTokenSource();

            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe(_nomeTopico);

                try
                {
                    while (true)
                    {
                        var cr = consumer.Consume(cts.Token);

                        if (cr != null && cr.Message != null)
                        {
                            var mensagem = cr.Message.Value;

                            var historico = JsonConvert.DeserializeObject<HistoricoDeClientes>(mensagem);

                            _historicoRepositorio.Create(historico);

                            logger.Information(
                                $"Mensagem lida: {cr.Message.Value}");
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

        private ConsumerConfig ObterConfiguracoes(string bootstrapServers, string groupId)
        {
            return new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Latest,
            };
        }
    }
}