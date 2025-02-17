using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MultiShop.RabbitMQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateMessage()
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
            };

            var connection = connectionFactory.CreateConnection();

            var channel = connection.CreateModel();

            channel.QueueDeclare("Növbə2", false, false, false, arguments: null);

            var messageContent = "Bu gün hava soyuqdur";

            var byteMessageContent = Encoding.UTF8.GetBytes(messageContent);

            channel.BasicPublish(exchange: "", routingKey: "Növbə2", basicProperties: null, body: byteMessageContent);


            return Ok("Mesajınız növbəyə alındı");
        }

        [HttpGet]
        public IActionResult ReadMessage()
        {
            string message = null;
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var consumer = new EventingBasicConsumer(channel);
                var messageReceived = new ManualResetEventSlim(false);

                consumer.Received += (model, ea) =>
                {
                    var byteMessage = ea.Body.ToArray();
                    message = Encoding.UTF8.GetString(byteMessage);
                    messageReceived.Set(); // Mesajın gəldiyini bildir
                };

                channel.BasicConsume(queue: "Növbə1", autoAck: true, consumer: consumer);

                messageReceived.Wait(3000);

                if (string.IsNullOrEmpty(message))
                {
                    return NoContent();
                }
                else
                {
                    return Ok(message);
                }
            }
        }

    }
}
