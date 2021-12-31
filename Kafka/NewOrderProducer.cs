using Confluent.Kafka;
using System.Threading.Tasks;

namespace Kafka
{
    public class NewOrderProducer
    {
        private readonly IProducer<string, string> producer;

        public NewOrderProducer()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            producer = new ProducerBuilder<string, string>(config).Build();
        }

        public Task<DeliveryResult<string, string>> Produce(
            string topic,
            string keyValue)
        {
            var message = new Message<string, string>
            {
                Key = keyValue,
                Value = keyValue
            };

            return producer.ProduceAsync(topic, message);
        }
    }
}
