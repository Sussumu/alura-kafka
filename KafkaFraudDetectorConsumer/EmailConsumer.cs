using Confluent.Kafka;
using System;

namespace KafkaConsumer
{
    public class EmailConsumer
    {
        private readonly IConsumer<string, string> consumer;

        public EmailConsumer()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = nameof(EmailConsumer)
            };

            consumer = new ConsumerBuilder<string, string>(config).Build();
        }

        public void Consume()
        {
            consumer.Subscribe("ECOMMERCE_SEND_EMAIL");

            try
            {
                while (true)
                {
                    var result = consumer.Consume(100);

                    if (result is null)
                        continue;

                    Console.WriteLine($"-------------------------");
                    Console.WriteLine($"EMAIL");
                    Console.WriteLine($"Sending email");
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