using fastwin.Requests;

namespace fastwin.Infrastructure.JWT
{
    public interface IAuthService
    {
        string GenerateJWTString(LoginReq user);
    }
}
