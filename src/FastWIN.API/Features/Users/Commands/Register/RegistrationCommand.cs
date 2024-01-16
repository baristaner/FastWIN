using fastwin.Entities;
using MediatR;

namespace fastwin.Requests.Users.Commands.Register
{
    public class RegisterUserCommand : IRequest<User>
    {
        public RegistrationRequest RegistrationRequest { get; }

        public RegisterUserCommand(RegistrationRequest registrationRequest)
        {
            RegistrationRequest = registrationRequest;
        }
    }
}
