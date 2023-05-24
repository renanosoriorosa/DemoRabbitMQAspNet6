using Models.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class EstoqueService
    {
        public void ProcessaPedido()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "pedidos",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            //Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    var pedido = System.Text.Json.JsonSerializer.Deserialize<PedidoViewModel>(message);

                    Console.WriteLine($" [x] Received {pedido.Id},{pedido.Codigo}, {pedido.Quantidade} ");

                    if (PossuiEstoque())
                        Console.WriteLine($" POSSUI ESTOQUE ");
                    else
                        Console.WriteLine($" NÂO POSSUI ESTOQUE ");

                    channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception e)
                {
                    channel.BasicNack(ea.DeliveryTag, false, true);
                }
            };

            channel.BasicConsume(queue: "pedidos",
                                 autoAck: false,
                                 consumer: consumer);
        }

        private bool PossuiEstoque()
        {
            Random randNum = new Random();
            int numero = randNum.Next(1, 200);

            if(NumeroPar(numero))
                return true;

            return false;
        }

        private bool NumeroPar(int numero)
        {
            if (numero % 2 == 0) return true;
            
            return false;
        }
    }
}
