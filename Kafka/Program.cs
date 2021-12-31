using System.Threading.Tasks;

namespace Kafka
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var producer = new Producer();

            var result = await producer.Produce();
        }
    }
}
