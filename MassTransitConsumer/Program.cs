using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace MassTransitConsumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddMassTransit(x =>
            {
                x.UsingInMemory((context, cfg) => cfg.ConfigureEndpoints(context));

                x.AddRider(rider =>
                {
                    rider.AddConsumer<KafkaMessageConsumer>();

                    rider.UsingKafka((context, k) =>
                    {
                        k.Host("localhost:9092");

                        k.TopicEndpoint<KafkaMessage>(
                            "ECOMMERCE_NEW_ORDER",
                            "consumer-group-name", e =>
                        {
                            e.CreateIfMissing(t =>
                            {
                                t.NumPartitions = 2;
                                t.ReplicationFactor = 1;
                            });

                            e.ConfigureConsumer<KafkaMessageConsumer>(context);
                        });
                    });
                });
            });

            Console.ReadKey();
        }
    }

    class KafkaMessageConsumer : IConsumer<KafkaMessage>
    {
        public Task Consume(ConsumeContext<KafkaMessage> context)
        {
            Console.WriteLine(context.Message.Message);
            return Task.CompletedTask;
        }
    }

    public interface KafkaMessage
    {
        string Message { get; }
    }
}
