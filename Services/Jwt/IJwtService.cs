namespace GCMS.Web.Services.Jwt
{
    public interface IJwtService
    {
        public string GenerateToken(int userID, string username, string role);
    }
}
