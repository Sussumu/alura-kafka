using KafkaConsumer;
using System;
using System.Threading.Tasks;

namespace KafkaFraudDetectorConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            // notas

            // para processar em paralelo, cada consumer fica responsável
            // por um número de partições em um tópico
            // havendo um número maior ou igual de partições do que de consumer groups,
            // o kafka rebalanceia automaticamente baseado na KEY da mensagem

            // dessa forma, para uma mesma chave, conseguimos processar sequencial

            // o padrão dessa lib do .net é fazer o commit de mensagens
            // uma a uma (https://github.com/confluentinc/confluent-kafka-dotnet/issues/1451)
            // por isso não é necessário alterar a quantidade máxima de mensagens
            // para evitar conflitos em casos de um batch muito grande acontecer
            // no meio de um rebalance

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
