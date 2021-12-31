using KafkaConsumer;
using System;
using System.Threading.Tasks;

namespace KafkaFraudDetectorConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var fraudDetectorConsumer = new FraudDetectorConsumer();
            var emailConsumer = new EmailConsumer();

            Task.Run(() => fraudDetectorConsumer.Consume());
            Task.Run(() => emailConsumer.Consume());

            Console.ReadKey();
        }
    }
}
