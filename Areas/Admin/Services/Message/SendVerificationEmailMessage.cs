namespace GCMS.Web.Areas.Admin.Services.Message
{
    public class SendVerificationEmailMessage
    {
        public string ToEmail { get; set; }
        public string Code { get; set; }
        public string Purpose { get; set; }
    }
}
