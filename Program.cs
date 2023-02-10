using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace RabbitMQ.Publisher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Ilk once RabbitMQ-ye baglanmaq ucun ConnectionFactoru classini new-lemek lazimdi
            var factoryConn = new ConnectionFactory();
            factoryConn.Uri = new Uri("amqps://isqfukxq:Kh6gMciYHMA6KfgIupsOGbhm9uLu8Gah@woodpecker.rmq.cloudamqp.com/isqfukxq");

            using var createConnection = factoryConn.CreateConnection();
            var channel = createConnection.CreateModel();

            channel.QueueDeclare("horse-queue", true, false, false);
            //("quyruq adi", bool durable = true/false(əgər false etsək butun quyruqlar yaddasda tutulur, əgər biz tətbiqə restart verəsək itəcək bu datalar. true versək əgər fiziki olaraq yadda saxlanilir və tətbiqə restart versək belə orada olacaq). bool exclusive = true/false (eger biz burada true versek, bu quyruq yalniz bizim yaratdigimiz kanala qosulacaq. Yox eger false versek, subcriber ozu istediyi bir kanali verer ve quyruq o kanala qosulacaq). bool autoDelete = true/false (eger true qeyd etsek, bizim son subcribemiz cixan zaman quyruq avtomatik silinecek. False olanda ise orada saxlanilacaq).


            Enumerable.Range(1, 100).ToList().ForEach(x =>
            {
                string message = $"Mesajiniz {x}";
                var messageBody = Encoding.UTF8.GetBytes(message);

                //Burada exchange isletmediyimiz ucun default exchange istifade edilir. Burada da quyruq adimizi veririk.
                channel.BasicPublish(string.Empty, "horse-queue", null, messageBody);

                Console.WriteLine($"Mesaj gonderildi: {message}");
            });

            Console.ReadLine();
        }
    }
}
