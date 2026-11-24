namespace GCMS.Web.Services.Redis
{
    public class RedisSetting
    {
        public string Host { get; set; } = "LocalHost";
        public int Port { get; set; } = 6379;
        public string Password { get; set; } = string.Empty;
    }
}
