using Confluent.Kafka;
using System;
using System.Threading;

namespace KafkaFraudDetectorConsumer
{
    public class FraudDetectorConsumer
    {
        private readonly IConsumer<string, string> consumer;

        public FraudDetectorConsumer()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = nameof(FraudDetectorConsumer)
            };

            consumer = new ConsumerBuilder<string, string>(config).Build();
        }

        public void Consume()
        {
            consumer.Subscribe("ECOMMERCE_NEW_ORDER");

            try
            {
                while (true)
                {
                    var result = consumer.Consume(100);

                    if (result is null)
                    {
                        continue;
                    }

                    Thread.Sleep(2000);

                    Console.WriteLine($"-------------------------");
                    Console.WriteLine($"key:\t\t{result.Message.Key}");
                    Console.WriteLine($"value:\t\t{result.Message.Value}");
                    Console.WriteLine($"partition:\t{result.Partition}");
                    Console.WriteLine($"offset:\t\t{result.Offset}");
                    Console.WriteLine($"-------------------------");
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"Ctrl+C pressed, consumer exiting");
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}
