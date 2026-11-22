using GCMS.Web.Areas.Admin.Services.Email;
using GCMS.Web.Models.Entities.Users;
using MailKit;
using MailKit.Net.Smtp;                             
using MailKit.Security;                              
using Microsoft.AspNetCore.Identity.UI.Services;   
using Microsoft.Extensions.Options;                  
using MimeKit;                                         

namespace GCMS.Services.Email
{
    public class IdentityEmailSender : IEmailSender   // پیاده‌سازی اینترفیس داخلی Identity
    {
        private readonly SmtpSetting _setting;       // نگهداری تنظیمات SMTP (Host, Port, Email, Password)
        private readonly User _user;

        public IdentityEmailSender(IOptions<SmtpSetting> settings) // DI تنظیمات رو تزریق می‌کنه
        {
            _setting = settings.Value;                // مقدار واقعی تنظیمات رو از IOptions می‌گیریم
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        // ↑ امضای متد دقیقاً همون چیزیه که Identity انتظار داره:
        //   email = گیرنده، subject = موضوع، htmlMessage = بدنه HTML
        {
            var message = new MimeMessage();           // ساخت یک پیام خالی
            message.From.Add(new MailboxAddress(_setting.SenderName, _setting.SenderEmail));
            // ↑ فرستنده: اسم نمایشی + ایمیل واقعی خودت (از appsettings)
            message.To.Add(new MailboxAddress("", _user.Email));
            // ↑ گیرنده: اسم خالی + ایمیل کاربر (پارامتر ورودی)
            message.Subject = subject;                 // موضوع ایمیل (مثلاً "کد ورود به GCMS")

            var bodyBuilder = new BodyBuilder { HtmlBody = htmlMessage };
            // ↑ ساخت بدنه با پشتیبانی از HTML
            message.Body = bodyBuilder.ToMessageBody(); // تبدیل بدنه به فرمت قابل ارسال

            using var client = new SmtpClient();       // ساخت کلاینت SMTP (using باعث dispose بعد از ارسال می‌شه)
            await client.ConnectAsync(_setting.Host, _setting.Port, SecureSocketOptions.StartTls);
            // ↑ اتصال به سرور SMTP با STARTTLS (رمزنگاری بعد از اتصال، معمولاً پورت 587)
            await client.AuthenticateAsync(_setting.SenderEmail, _setting.Password);
            // ↑ لاگین به اکانت ایمیل فرستنده با App Password
            await client.SendAsync(new SendRequest(message));   // ارسال پیام
            await client.DisconnectAsync(true);        // قطع اتصال (true = ارسال QUIT به سرور)
        }
    }
}