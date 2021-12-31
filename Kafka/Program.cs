using System.Threading.Tasks;

namespace Kafka
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var producer = new NewOrderProducer();

            var orderRecordResult = await producer.Produce(
                "ECOMMERCE_NEW_ORDER",
                "123,321,1.99");

            var emailRecordResult = await producer.Produce(
                "ECOMMERCE_SEND_EMAIL",
                "Thanks for your order!");
        }
    }
}
