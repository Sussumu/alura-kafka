﻿namespace KafkaFraudDetectorConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var consumer = new FraudDetectorConsumer();

            consumer.Consume();
        }
    }
}