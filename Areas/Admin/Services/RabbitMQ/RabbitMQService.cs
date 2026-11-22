using GCMS.Web.Services.RabbitMQ;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        public RabbitMQService(IOptions<RabbitMQSettings> RabbitMQSettings)
        {
            _setting = RabbitMQSettings.Value;
            _factory = new ConnectionFactory
            {
                HostName = _setting.HostName,
                Port = _setting.Port,
                UserName = _setting.UserName,
                Password = _setting.Password
            };
        }
        public async Task ConnectionAsync()
        {
            _connection = await _factory.CreateConnectionAsync();
        }
        public async Task PublishAsync(string RabbitQ, string message)
        {
            if (_connection == null || !_connection.IsOpen)
                await ConnectionAsync();
            await using IChannel channel = await _connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(
                    queue: RabbitQ,
                    durable: false,
                    exclusive: false,
                    arguments: null
                );
            var body = Encoding.UTF8.GetBytes(message);
            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: RabbitQ,
                body: body
                );

        }
    }
}