using fastwin.Requests;

namespace fastwin.Infrastructure.JWT
{
    public interface IAuthService
    {
        Task <string> GenerateJWTString(LoginReq user);
    }
}
