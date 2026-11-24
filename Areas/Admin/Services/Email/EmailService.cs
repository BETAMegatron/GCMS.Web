using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace GCMS.Web.Areas.Admin.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _settings;
        public EmailService(IOptions<SmtpSettings> SmtpSettings)
        {
            _settings = SmtpSettings.Value;
        }
        public async Task SendVerificationCode(string toEmail, string code, string purpose)
        {
            //ایـجاد یک پیام 
            var message = new MimeMessage(); 
            message.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
            message.To.Add(new MailboxAddress("", toEmail));

            message.Subject = purpose == "Login"
                ? "GCMS - کد ورود شما"
                : "GCMS - کد تغییر رمز عبور شما";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $@"
                    <div style='font-family: Tahoma, sans-serif; direction: rtl;'>
                        <h3>سیستم GCMS</h3>
                        <p>کد تأیید شما برای {purpose} عبارت است از:</p>
                        <h2 style='color: #007bff; letter-spacing: 5px;'>{code}</h2>
                        <p>این کد تا ۵ دقیقه دیگر منقضی می‌شود.</p>
                    </div>"
            };
            message.Body = bodyBuilder.ToMessageBody();
            using var client = new SmtpClient();
            await client.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_settings.SenderEmail, _settings.Password);
            await client.SendAsync(new SendRequest(message));
            await client.DisconnectAsync(true);
        }
    }
}
