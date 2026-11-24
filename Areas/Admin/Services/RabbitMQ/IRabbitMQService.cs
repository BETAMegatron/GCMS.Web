using Azure.Messaging;
using GCMS.Web.Areas.Admin.Services.Message;

namespace GCMS.Web.Areas.Admin.Services.RabbitMQ
{
    public interface IRabbitMQService
    {
      Task PublishAsync(string RabbitQ, SendVerificationEmailMessage message);
      
    }
}
