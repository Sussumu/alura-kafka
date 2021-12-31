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
            var loggerConsumer = new LoggerConsumer();

            Task.Run(() => fraudDetectorConsumer.Consume());
            Task.Run(() => emailConsumer.Consume());
            Task.Run(() => loggerConsumer.Consume());

            Console.ReadKey();
        }
    }
}
