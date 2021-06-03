using CalixtosStore.Email.Cliente;
using Confluent.Kafka;
using Newtonsoft.Json;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Threading;

namespace CalixtosStore.Email
{
    public class Consumer
    {
        public void Execute()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
                .CreateLogger();
            logger.Information("Testando o consumo de mensagens com Kafka");

            string bootstrapServers = "localhost:9092";
            string nomeTopic = "registrar-cliente";
            string groupId = "hiperDev";

            logger.Information($"BootstrapServers = {bootstrapServers}");
            logger.Information($"Topic = {nomeTopic}");
            logger.Information($"Group Id = {groupId}");

            var config = ObterConfiguracoes(bootstrapServers, groupId);

            var cts = new CancellationTokenSource();

            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe(nomeTopic);

                try
                {
                    while (true)
                    {
                        var cr = consumer.Consume(cts.Token);

                        if (cr != null && cr.Message != null)
                        {
                            var mensagem = cr.Message.Value;

                            var cliente = JsonConvert.DeserializeObject<NovoCliente>(mensagem);

                            var emailSender = new AuthMessageSender();

                            emailSender.SendEmailAsync(cliente.Email, $"Bem vindo! {cliente.Nome}", $"Bem vindo ao nosso sistema, {cliente.Nome}! Esse é um teste para o HiperDev!");

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
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };
        }
    }
}