namespace GCMS.Web.Areas.Admin.Services.Email
{
    public class SmtpSetting
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public string SenderEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
