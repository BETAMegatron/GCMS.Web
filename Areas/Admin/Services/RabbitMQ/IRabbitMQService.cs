using Azure.Messaging;

namespace GCMS.Web.Areas.Admin.Services.RabbitMQ
{
    public interface IRabbitMQService
    {
      Task PublishAsync(string RabbitQ, string message);
    }
}
