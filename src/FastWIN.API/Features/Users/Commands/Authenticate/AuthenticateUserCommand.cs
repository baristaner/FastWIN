using MediatR;
using fastwin.Entities;
using fastwin.Requests;

public class AuthenticateUserCommand : IRequest<User>
{
    public LoginReq LoginRequest { get; }

    public string Email { get; }
    public string Password { get; }

    public AuthenticateUserCommand(LoginReq loginRequest)
    {
        Email = loginRequest.Email;
        Password = loginRequest.Password;
        LoginRequest = loginRequest;
    }
}
