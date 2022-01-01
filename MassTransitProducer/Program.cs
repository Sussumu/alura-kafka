using MassTransit;
using MassTransit.KafkaIntegration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MassTransitProducer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddMassTransit(x =>
            {
                x.UsingInMemory((context, cfg) => cfg.ConfigureEndpoints(context));

                x.AddRider(rider =>
                {
                    rider.AddProducer<KafkaMessage>("ECOMMERCE_NEW_ORDER");

                    rider.UsingKafka((context, k) =>
                    {
                        k.Host("localhost:9092");
                    });
                });
            });

            var provider = services.BuildServiceProvider();

            var busControl = provider
                .GetRequiredService<IBusControl>();

            await busControl.StartAsync(
                new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token);

            try
            {
                var producer = provider
                    .GetRequiredService<ITopicProducer<KafkaMessage>>();
                do
                {
                    string value = await Task.Run(() =>
                    {
                        Console.WriteLine("Enter text (or quit to exit)");
                        Console.Write("> ");
                        return Console.ReadLine();
                    });

                    if ("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
                        break;

                    await producer.Produce(new
                    {
                        Text = value
                    });
                }
                while (true);
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }

    public interface KafkaMessage
    {
        string Message { get; }
    }
}
