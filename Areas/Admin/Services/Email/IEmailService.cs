namespace GCMS.Web.Areas.Admin.Services.Email
{
    public interface IEmailService
    {
        public Task SendVerificationCode(string toemail, string code, string purpose);
    }
}
