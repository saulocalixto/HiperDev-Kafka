using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Ms_HistoricoClientes.Data.Repositorios;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ms_HistoricoClientes_KafkaConsumer.Services
{
    public class Consumer : BackgroundService
    {
        private readonly HistoricoDeClientesRepositorio _historicoRepositorio;
        private const string _nomeTopico = "registrar-historico-cliente";
        private const string _grupoId = "HiperDevHistorico";
        private const string _bootstrapServer = "localhost:9092";

        public Consumer(HistoricoDeClientesRepositorio historicoRepositorio)
        {
            _historicoRepositorio = historicoRepositorio;
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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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

            await Task.Run(() =>
            {
                using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
                {
                    consumer.Subscribe(_nomeTopico);

                    try
                    {
                        while (true)
                        {
                            try
                            {
                                //var cr = consumer.Consume(cts.Token);

                                //if (cr != null && cr.Message != null)
                                //{
                                //    var mensagem = cr.Message.Value;

                                //    var historico = JsonConvert.DeserializeObject<HistoricoDeClientes>(mensagem);

                                //    _historicoRepositorio.Create(historico);

                                //    logger.Information(
                                //        $"Mensagem lida: {cr.Message.Value}");
                                //}
                                Console.WriteLine("Saulo");
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
            });
        }
    }
}