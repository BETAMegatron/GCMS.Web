using GCMS.Web.Services.RabbitMQ;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace GCMS.Web.Areas.Admin.Services.RabbitMQ
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly RabbitMQSettings _setting;
        private readonly ConnectionFactory _factory;
        private IConnection _connection;
        private async Task ConnectAsync()
        {
            _connection = await _factory.CreateConnectionAsync();
        }

        public RabbitMQService(IOptions<RabbitMQSettings> oprion)
        {
            _setting = oprion.Value;
            _factory = new ConnectionFactory
            {
                HostName = _setting.HostName,
                Port = _setting.Port,
                UserName = _setting.UserName,
                Password = _setting.Password,
            };
        }
        public async Task PublishAsync(string GCMSRabbitQ, string message)
        {
            if (_connection == null || !_connection.IsOpen)
            {
                await ConnectAsync();
            }
            await using IChannel channel = await _connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: GCMSRabbitQ,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );
            var body = Encoding.UTF8.GetBytes(message);
            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: GCMSRabbitQ,
                body: body
                );
        }
    }
}
