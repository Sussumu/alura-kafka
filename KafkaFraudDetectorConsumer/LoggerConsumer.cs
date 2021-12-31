using Confluent.Kafka;
using System;

namespace KafkaConsumer
{
    public class LoggerConsumer
    {
        private readonly IConsumer<string, string> consumer;

        public LoggerConsumer()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = nameof(LoggerConsumer),
                AllowAutoCreateTopics = true
            };

            consumer = new ConsumerBuilder<string, string>(config).Build();
        }

        public void Consume()
        {
            consumer.Subscribe("^ECOMMERCE.*");

            try
            {
                while (true)
                {
                    var result = consumer.Consume(100);

                    if (result is null)
                        continue;

                    Console.WriteLine($"-------------------------");
                    Console.WriteLine($"LOG {result.Topic}");
                    Console.WriteLine($"value:\t\t{result.Message.Value}");
                    Console.WriteLine($"partition:\t{result.Partition}");
                    Console.WriteLine($"offset:\t\t{result.Offset}");
                    Console.WriteLine($"-------------------------\n");
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
