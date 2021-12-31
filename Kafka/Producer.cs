using Confluent.Kafka;
using System.Threading.Tasks;

namespace Kafka
{
    public class Producer
    {
        private readonly IProducer<string, string> producer;

        public Producer()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            producer = new ProducerBuilder<string, string>(config).Build();
        }

        public Task<DeliveryResult<string, string>> Produce()
        {
            var order = "123,321,1.99";

            var message = new Message<string, string>
            {
                Key = order,
                Value = order
            };

            return producer.ProduceAsync("ECOMMERCE_NEW_ORDER", message);
        }
    }
}
